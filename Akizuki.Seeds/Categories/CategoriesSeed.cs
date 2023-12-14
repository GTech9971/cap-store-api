using Akizuki.Domain.Catalogs;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CapStore.Domain.Categories;

namespace Akizuki.Seeds;

public class CategoriesSeed
{
    public CategoriesSeed() { }

    /// <summary>
    /// 秋月電子のページから大項目のカテゴリー名をすべて取得する
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CategoryName>> FetchCategoryNamesFromAkizukiPage(AkizukiPageUrl url)
    {
        using (IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader()))
        {
            IDocument document = await context.OpenAsync(url.Value);
            HtmlParser parser = new HtmlParser();
            IDocument parsedDocument = await parser.ParseDocumentAsync(document);

            IHtmlCollection<IElement> elements = parsedDocument.GetElementsByTagName("div");
            IEnumerable<CategoryName> categoryElements =
                elements
                    .Where(x => x.Id == "cate-01" && x.InnerHtml.Contains("<a href="))
                    .Select(x => new CategoryName(
                        x.TextContent.Replace("\n", "").Replace("\t", "")
                        )
                    );

            return categoryElements;
        }
    }
}
