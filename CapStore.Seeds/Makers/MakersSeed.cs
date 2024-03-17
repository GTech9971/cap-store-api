using System.Text;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Makers;

namespace CapStore.Seeds.Makers;

public class MakersSeed
{
    private const string PATH = "../../../../CapStore.Seeds/Assets/makers.txt";

    public MakersSeed() { }

    /// <summary>
    /// 秋月電子のページからメーカー名をすべて取得する
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<IEnumerable<MakerName>> FetchMakerNamesFromAkizukiPage(AkizukiPageUrl url)
    {
        using (IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader()))
        {
            IDocument document = await context.OpenAsync(url.Value);
            HtmlParser parser = new HtmlParser();
            IDocument parsedDocument = await parser.ParseDocumentAsync(document);

            IHtmlCollection<IElement> elements = parsedDocument.GetElementsByClassName("menu03");
            IEnumerable<MakerName> makerElements =
                elements
                    .Where(x => x.ChildElementCount > 0)
                    .Select(x => x.Children.FirstOrDefault())
                    .Where(x =>
                    {
                        string? href = x.GetAttribute("href");
                        if (href == null) { return false; }
                        return href.Contains("?maker=");
                    })
                    .Select(x => new MakerName(
                        x.TextContent.Replace("\n", "").Replace("\t", "").Replace("　", "").Trim()
                        )
                    );

            return makerElements;
        }
    }

    /// <summary>
    /// 秋月電子から取得したメーカー名をファイルに書き込む
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<string> SaveAsync(AkizukiPageUrl url)
    {
        using (StreamWriter writer = new StreamWriter(PATH, false, Encoding.UTF8))
        {
            IEnumerable<MakerName> categoryNames = await FetchMakerNamesFromAkizukiPage(url);

            List<MakerName> list = categoryNames.ToList();
            foreach (var name in list)
            {
                await writer.WriteLineAsync(name.ToString());
            }
            await writer.FlushAsync();

            return PATH;
        }
    }


    /// <summary>
    /// テキストからメーカー名を取得する
    /// </summary>
    /// <returns></returns>
    public async Task<List<MakerName>> FetchMakerFromTxtAsync()
    {
        List<MakerName> makerNames = new List<MakerName>();
        using (StreamReader reader = new StreamReader(PATH, Encoding.UTF8))
        {
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                MakerName makerName = new MakerName(line);
                makerNames.Add(makerName);
            }
        }

        return makerNames;
    }

}
