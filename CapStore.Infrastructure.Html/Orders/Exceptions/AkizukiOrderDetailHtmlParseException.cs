namespace CapStore.Infrastructure.Html.Orders.Exceptions;

/// <summary>
/// 秋月電子の注文詳細ページ解析時の例外
/// </summary>
public class AkizukiOrderDetailHtmlParseException : Exception
{
    public AkizukiOrderDetailHtmlParseException()
    : base($"秋月電子の注文詳細ページの解析に失敗しました。") { }
}
