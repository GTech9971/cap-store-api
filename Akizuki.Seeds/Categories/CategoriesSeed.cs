using System.Text;
using Akizuki.Domain.Catalogs;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CapStore.Domain.Categories;

namespace Akizuki.Seeds;

public class CategoriesSeed
{

    private const string PATH = "../../../Assets/categories.txt";

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

            IHtmlCollection<IElement> elements = parsedDocument.GetElementsByClassName("sidenv_tree1_");
            IEnumerable<CategoryName> categoryElements =
                elements
                    .Where(x => x.ChildElementCount > 0)
                    .Select(x => x.Children.FirstOrDefault())
                    .Where(x => string.IsNullOrWhiteSpace(x.TextContent) == false)
                    .Select(x => new CategoryName(
                        x.TextContent.Replace("\n", "").Replace("\t", "")
                        )
                    );

            return categoryElements;
        }
    }


    /// <summary>
    /// 秋月電子から取得したカテゴリー名をファイルに書き込む
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<string> SaveAsync(AkizukiPageUrl url)
    {
        using (StreamWriter writer = new StreamWriter(PATH, false, Encoding.UTF8))
        {
            IEnumerable<CategoryName> categoryNames = await FetchCategoryNamesFromAkizukiPage(url);
            List<CategoryName> list = categoryNames.ToList();
            foreach (var name in list)
            {
                await writer.WriteLineAsync(name.ToString());
            }
            await writer.FlushAsync();

            return PATH;
        }
    }
}
