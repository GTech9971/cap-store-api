using System.Text;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Categories;

namespace CapStore.Seeds.Categories;

public class CategoriesSeed
{

    private const string PATH = "../../../../CapStore.Seeds/Assets/categories.txt";

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

            IReadOnlyCollection<CategoryName> ignoreList = new List<CategoryName>(){
                new CategoryName("ﾍﾞｽﾄｾﾗｰﾊﾟｰﾂ"),
                new CategoryName("新商品"),
                new CategoryName("会社別"),
                new CategoryName("会社別(旧版)"),
                new CategoryName("目的別"),
                new CategoryName("再入荷商品")
            }.AsReadOnly();

            return categoryElements.Where(x => ignoreList.Contains(x) == false);
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

    /// <summary>
    /// テキストからカテゴリー名を取得する
    /// </summary>
    /// <returns></returns>
    public async Task<List<CategoryName>> FetchCategoriesFromTxtAsync()
    {
        List<CategoryName> categoryNames = new List<CategoryName>();
        using (StreamReader reader = new StreamReader(PATH, Encoding.UTF8))
        {
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                CategoryName categoryName = new CategoryName(line);
                categoryNames.Add(categoryName);
            }
        }

        return categoryNames;
    }
}
