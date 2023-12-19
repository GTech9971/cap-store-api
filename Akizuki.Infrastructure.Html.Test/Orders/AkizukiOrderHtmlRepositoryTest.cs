using System.Text;
using Akizuki.Domain.Orders;

namespace Akizuki.Infrastructure.Html.Test;

public class AkizukiOrderHtmlRepositoryTest
{
    private readonly AkizukiOrderHtmlRepository _repository;

    public AkizukiOrderHtmlRepositoryTest()
    {
        _repository = new AkizukiOrderHtmlRepository();
    }

    [Fact(DisplayName = "注文情報取得成功テスト")]
    [Trait("Category", "Akizuki")]
    public async Task TestSuccess()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        string html = await File.ReadAllTextAsync("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/orders.html", Encoding.GetEncoding("SHIFT_JIS"));
        IEnumerable<IOrder> orders = await _repository.Fetch(html);

        Assert.True(html.Any());
        Assert.True(orders.Any());
        Assert.Equal(15, orders.Count());
    }
}
