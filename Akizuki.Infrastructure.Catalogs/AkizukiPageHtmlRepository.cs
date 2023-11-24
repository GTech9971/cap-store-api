using System;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.XPath;
using Akizuki.Domain.Catalogs;
using CapStore.Domain.Components;
using System.Text.RegularExpressions;

namespace Akizuki.Infrastructure.Catalogs.Html
{
	public class AkizukiPageHtmlRepository : IAzikzukiPageRepository
	{
		public AkizukiPageHtmlRepository()
		{
		}

		private ComponentModelName ParseModelName(IDocument document)
		{
            INode modelNameNode = document.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td");

			int startIndex = modelNameNode.TextContent.IndexOf("[") + 1;
			int length =  modelNameNode.TextContent.IndexOf("]") - startIndex;
			string modelNameStr = modelNameNode.TextContent.Substring(startIndex, length);
            ComponentModelName modelName = new ComponentModelName(modelNameStr);

			return modelName;
        }

		private ComponentDescription PartseDescription(IDocument document)
		{
            //説明
            INode descriptionHeadNode = document.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[3]/td/text()");
            string descriptionHeadStr = descriptionHeadNode.TextContent.Replace("\t", "").Replace("\n", "");

            INode descriptionBodyNode = document.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[4]/td");
            string descriptionBodyStr = descriptionBodyNode.TextContent.Replace("\t","").Replace("\n", "");
            ComponentDescription description = new ComponentDescription($"{descriptionHeadStr}\n{descriptionBodyStr}");

			return description;
        }

		private ComponentImages PartseComponentImages(IDocument document)
		{
			const string PATTERN = "\\/img\\/goods\\/[A-Z][1-9]\\/[A-Z]-d{5}\\.jpg";
			//画像
			var imageTableNode = document.Body.GetElementsByTagName("a")
												.Where(x => Regex.IsMatch(x.GetAttribute("href"), PATTERN));
			
			foreach(var node in imageTableNode)
			{
				Console.WriteLine(node);
			}

			return new ComponentImages();
		}

        public async Task<AkizukiPage> FetchAkizukiPageAsync(AkizukiCatalogPageUrl url)
        {			
			using (IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader()))
			{				
				IDocument document = await context.OpenAsync(url.Value);
				HtmlParser parser = new HtmlParser();
				IDocument parsedDocument = await parser.ParseDocumentAsync(document);

				//電子部品名
				INode titleNode = parsedDocument.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/h6");
				string componentNameStr = titleNode.TextContent;
                ComponentName name = new ComponentName(componentNameStr);

				//モデル名
				ComponentModelName modelName = ParseModelName(parsedDocument);

				//説明
				ComponentDescription description = PartseDescription(parsedDocument);


				//カテゴリー
				INode categoryNameNode = parsedDocument.Body.SelectSingleNode("/html/body/div/div[2]/div[1]/div/a[2]");
				string categoryNameStr = categoryNameNode.TextContent;
				CategoryName categoryName = new CategoryName(categoryNameStr);
				Category category = new Category(CategoryId.UnDetectId(), categoryName, null);

				//メーカー
				INode makerNameNode = parsedDocument.Body.SelectSingleNode("/html/body/div/div[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/span/a");
				string makerNameStr = makerNameNode.TextContent;				
				MakerName makerName = new MakerName(makerNameStr);
				Maker maker = new Maker(MakerId.UnDetect(), makerName, null);


				ComponentImages componentImages = PartseComponentImages(parsedDocument);

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
    }
}

