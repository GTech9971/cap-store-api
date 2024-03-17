using System;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.XPath;
using CapStore.Domains.Components;
using System.Text.RegularExpressions;
using CapStore.Domains.Makers;
using CapStore.Domains.Categories;
using CapStore.Domains.Shareds;
using System.Text;
using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Infrastructure.Html.Catalogs.Exceptions;

namespace CapStore.Infrastructure.Html.Catalogs
{
	public class AkizukiPageHtmlRepository : IAkizukiPageRepository
	{
		private readonly HtmlParser parser;
		private readonly string ROOT_PATH = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			 "CapStore",
			  "Akizuki",
			   "Catalogs",
				"Assets");

		public AkizukiPageHtmlRepository()
		{
			parser = new HtmlParser();
		}

		private ComponentModelName ParseModelName(IDocument document)
		{
			INode? modelNameNode = document.Body.SelectSingleNode("//*[@id='spec_number']");
			if (modelNameNode == null)
			{
				throw new AkizukiPageHtmlParseException();
			}

			string modelNameStr = modelNameNode.TextContent;
			if (string.IsNullOrWhiteSpace(modelNameStr))
			{
				return ComponentModelName.None();
			}
			else
			{
				return new ComponentModelName(modelNameStr);
			}
		}

		private ComponentDescription ParseDescription(IDocument document)
		{
			//説明
			INode? descriptionHeadNode = document.Body.SelectSingleNode("/html/body/div[1]/div[2]/div/main/div/div[4]/div[1]");
			if (descriptionHeadNode == null)
			{
				throw new AkizukiPageHtmlParseException();
			}
			string descriptionHeadStr = descriptionHeadNode.TextContent.Replace("\t", "").Replace("\n", "");

			INode? descriptionBodyNode = document.Body.SelectSingleNode("/html/body/div[1]/div[2]/div/main/div/div[4]/div[5]");
			if (descriptionBodyNode == null)
			{
				throw new AkizukiPageHtmlParseException();
			}
			string descriptionBodyStr = descriptionBodyNode.TextContent.Replace("\t", "").Replace("\n", "");
			ComponentDescription description = new ComponentDescription($"{descriptionHeadStr}\n{descriptionBodyStr}");

			return description;
		}

		private Maker ParseMaker(IDocument document)
		{
			if (document.Body == null)
			{
				throw new AkizukiPageHtmlParseException();
			}
			//メーカー
			var maker = document.Body.GetElementsByClassName("goods-detail-description block-goods-detail-maker");
			if (maker == null || maker.Any() == false)
			{
				return Maker.None();
			}
			string? makerNameStr = maker
								.First()
								.GetElementsByTagName("a")
								.FirstOrDefault()
								?.TextContent;

			if (makerNameStr == null)
			{
				throw new AkizukiPageHtmlParseException();
			}
			return new Maker(MakerId.UnDetect(), new MakerName(makerNameStr), null);
		}

		private ComponentImageList ParseComponentImages(IDocument document, CatalogId catalogId)
		{
			if (document.Body == null)
			{
				throw new AkizukiPageHtmlParseException();
			}

			var gallery = document.Body.GetElementsByClassName("block-goods-gallery");
			if (gallery == null || gallery.Any() == false)
			{
				throw new AkizukiPageHtmlParseException();
			}

			IEnumerable<ComponentImage> imageUrls = gallery
												.First()
												.GetElementsByTagName("a")
												.Select(x => x.GetAttribute("href"))
												.Where(x => x != null)
												.Distinct()
												.Select(x => new AkizukiPageUrl(x!))
												.Where(x => Regex.IsMatch(x.Value, AkizukiImageUrl.PATTERN))
												.Select(x => ComponentImage.UnDetectId(new ImageUrl(x.Value)));


			return new ComponentImageList(imageUrls);
		}


		private async Task SaveHtmlAsync(IDocument document, CatalogId catalogId)
		{
			if (Directory.Exists(ROOT_PATH) == false)
			{
				Directory.CreateDirectory(ROOT_PATH);
			}
			string path = Path.Combine(ROOT_PATH, $"{catalogId.Value}.html");

			if (File.Exists(path)) { return; }

			string html = document.Source.Text;
			await File.WriteAllTextAsync(path, html, Encoding.UTF8);
		}

		private void DeleteHtml(CatalogId catalogId)
		{
			string path = Path.Combine(ROOT_PATH, $"{catalogId.Value}.html");

			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		private async Task<string?> LoadHtmlAsync(CatalogId catalogId)
		{
			string path = Path.Combine(ROOT_PATH, $"{catalogId.Value}.html");
			if (File.Exists(path) == false)
			{
				return null;
			}

			return await File.ReadAllTextAsync(path, Encoding.UTF8);
		}

		/// <summary>
		/// 秋月電子のページを解析する
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		/// <exception cref="AkizukiPageHtmlParseException"></exception>
		public async Task<AkizukiPage> FetchAkizukiPageAsync(AkizukiCatalogPageUrl url)
		{
			using (IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader()))
			{

				string? html = await LoadHtmlAsync(url.CatalogId);

				IDocument document = html == null
					? await context.OpenAsync(url.Value)
					: await context.OpenAsync(req => req.Content(html));

				await SaveHtmlAsync(document, url.CatalogId);

				IDocument parsedDocument = html == null
					? await parser.ParseDocumentAsync(document)
					: await parser.ParseDocumentAsync(html);


				//電子部品名
				INode? titleNode = parsedDocument.Body.SelectSingleNode("/html/body/div[1]/div[2]/div/main/div/div[1]/div[1]/h1");
				if (titleNode == null)
				{
					if (parsedDocument.Body == null)
					{
						throw new AkizukiPageHtmlParseException(url);
					}

					//販売終了の可能性あり
					if (parsedDocument.Body.TextContent.Contains("ご指定の商品は販売終了か、ただ今お取扱いできない商品です。"))
					{
						DeleteHtml(url.CatalogId);
						throw new AkizukiCatalogIdUnAvailableException(url.CatalogId, "販売終了または、取り扱い不可能");
					}

					//それ以外の場合は例外終了
					throw new AkizukiPageHtmlParseException(url);
				}
				string componentNameStr = titleNode.TextContent.Replace("　", " ");
				ComponentName name = new ComponentName(componentNameStr);

				//モデル名
				ComponentModelName modelName = ParseModelName(parsedDocument);

				//説明
				ComponentDescription description = ParseDescription(parsedDocument);


				//カテゴリー
				INode? categoryNameNode = parsedDocument.Body.SelectSingleNode("//*[@id='bread-crumb-list']/li[2]/a/span");
				if (categoryNameNode == null)
				{
					throw new AkizukiPageHtmlParseException(url);
				}
				string categoryNameStr = categoryNameNode.TextContent;
				CategoryName categoryName = new CategoryName(categoryNameStr);
				Category category = new Category(CategoryId.UnDetectId(), categoryName, null);

				//メーカー
				Maker maker = ParseMaker(parsedDocument);

				ComponentImageList componentImages = ParseComponentImages(parsedDocument, url.CatalogId);

				Component component = new Component(ComponentId.UnDetectId(),
													name,
													modelName,
													description,
													category,
													maker,
													componentImages);

				return new AkizukiPage(url, component);
			}
		}

		/// <summary>
		/// 秋月電子のページを解析する
		/// </summary>
		/// <param name="catalogId"></param>
		/// <returns></returns>
		/// <exception cref="AkizukiPageHtmlParseException"></exception>
		/// <exception cref="AkizukiCatalogIdUnAvailableException"></exception>
		public Task<AkizukiPage> FetchAkizukiPageAsync(CatalogId catalogId)
		{
			return FetchAkizukiPageAsync(new AkizukiCatalogPageUrl(catalogId));
		}
	}
}

