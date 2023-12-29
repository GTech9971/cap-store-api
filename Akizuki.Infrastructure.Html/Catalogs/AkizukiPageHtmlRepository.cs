using System;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.XPath;
using Akizuki.Domain.Catalogs;
using CapStore.Domain.Components;
using System.Text.RegularExpressions;
using CapStore.Domain.Makers;
using CapStore.Domain.Categories;
using CapStore.Domain.Shareds;
using System.Text;

namespace Akizuki.Infrastructure.Catalogs.Html
{
	public class AkizukiPageHtmlRepository : IAzikzukiPageRepository
	{
		private readonly HtmlParser parser;
		private const string ROOT_PATH = "../../../../Akizuki.Infrastructure.Html/Catalogs/Assets";

		public AkizukiPageHtmlRepository()
		{
			parser = new HtmlParser();
		}

		private ComponentModelName ParseModelName(IDocument document)
		{
			INode? modelNameNode = document.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td");
			if (modelNameNode == null)
			{
				throw new AkizukiPageHtmlParseException();
			}

			int startIndex = modelNameNode.TextContent.IndexOf("[") + 1;
			int length = modelNameNode.TextContent.IndexOf("]") - startIndex;
			string modelNameStr = modelNameNode.TextContent.Substring(startIndex, length);
			ComponentModelName modelName = new ComponentModelName(modelNameStr);

			return modelName;
		}

		private ComponentDescription PartseDescription(IDocument document)
		{
			//説明
			INode? descriptionHeadNode = document.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[3]/td/text()");
			if (descriptionHeadNode == null)
			{
				throw new AkizukiPageHtmlParseException();
			}
			string descriptionHeadStr = descriptionHeadNode.TextContent.Replace("\t", "").Replace("\n", "");

			INode? descriptionBodyNode = document.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[4]/td");
			if (descriptionBodyNode == null)
			{
				throw new AkizukiPageHtmlParseException();
			}
			string descriptionBodyStr = descriptionBodyNode.TextContent.Replace("\t", "").Replace("\n", "");
			ComponentDescription description = new ComponentDescription($"{descriptionHeadStr}\n{descriptionBodyStr}");

			return description;
		}

		private ComponentImageList PartseComponentImages(IDocument document, CatalogId catalogId)
		{
			if (document.Body == null)
			{
				throw new AkizukiPageHtmlParseException();
			}

			//画像
			IEnumerable<ComponentImage> imageUrls = document.Body.GetElementsByTagName("img")
												.Select(x => x.GetAttribute("src"))
												.Where(x => x.Contains(catalogId.Value))
												.Distinct()
												.Select(x => new AkizukiPageUrl(x))
												.Where(x => Regex.IsMatch(x.Value, AkizukiImageUrl.PATTERN))
												.Select(x => ComponentImage.UnDetectId(new ImageUrl(x.Value)));



			return new ComponentImageList(imageUrls);
		}


		private async Task SaveHtmlAsync(IDocument document, CatalogId catalogId)
		{
			string path = Path.Combine(ROOT_PATH, $"{catalogId.Value}.html");

			if (File.Exists(path)) { return; }

			string html = document.Source.Text;
			await File.WriteAllTextAsync(path, html, Encoding.GetEncoding("SHIFT_JIS"));
		}

		private async Task<string?> LoadHtmlAsync(CatalogId catalogId)
		{
			string path = Path.Combine(ROOT_PATH, $"{catalogId.Value}.html");
			if (File.Exists(path) == false)
			{
				return null;
			}

			return await File.ReadAllTextAsync(path, Encoding.GetEncoding("SHIFT_JIS"));
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

				IDocument parsedDocument = await parser.ParseDocumentAsync(document);


				//電子部品名
				INode? titleNode = parsedDocument.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/h6");
				if (titleNode == null)
				{
					throw new AkizukiPageHtmlParseException(url);
				}
				string componentNameStr = titleNode.TextContent;
				ComponentName name = new ComponentName(componentNameStr);

				//モデル名
				ComponentModelName modelName = ParseModelName(parsedDocument);

				//説明
				ComponentDescription description = PartseDescription(parsedDocument);


				//カテゴリー
				INode? categoryNameNode = parsedDocument.Body.SelectSingleNode("/html/body/div/div[2]/div[1]/div/a[2]");
				if (categoryNameNode == null)
				{
					throw new AkizukiPageHtmlParseException(url);
				}
				string categoryNameStr = categoryNameNode.TextContent;
				CategoryName categoryName = new CategoryName(categoryNameStr);
				Category category = new Category(CategoryId.UnDetectId(), categoryName, null);

				//メーカー
				INode? makerNameNode = parsedDocument.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/span/a");
				if (makerNameNode == null)
				{
					throw new AkizukiPageHtmlParseException(url);
				}
				string makerNameStr = makerNameNode.TextContent;
				MakerName makerName = new MakerName(makerNameStr);
				Maker maker = new Maker(MakerId.UnDetect(), makerName, null);


				ComponentImageList componentImages = PartseComponentImages(parsedDocument, url.CatalogId);

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
		public Task<AkizukiPage> FetchAkizukiPageAsync(CatalogId catalogId)
		{
			return FetchAkizukiPageAsync(new AkizukiCatalogPageUrl(catalogId));
		}
	}
}

