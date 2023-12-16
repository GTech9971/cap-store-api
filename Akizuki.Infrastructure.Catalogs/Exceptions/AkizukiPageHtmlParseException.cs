using Akizuki.Domain.Catalogs;

namespace Akizuki.Infrastructure.Catalogs.Html;

/// <summary>
/// 秋月電子のページ解析時の例外
/// </summary>
public class AkizukiPageHtmlParseException : Exception
{
    public AkizukiPageHtmlParseException()
        : base($"秋月電子のページの解析に失敗しました。") { }

    public AkizukiPageHtmlParseException(AkizukiCatalogPageUrl url)
    : base($"秋月電子のページの解析に失敗しました。{url.Value}") { }
}
