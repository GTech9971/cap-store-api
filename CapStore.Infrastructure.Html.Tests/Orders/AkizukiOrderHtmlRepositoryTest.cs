using System.Text;
using CapStore.Domains.Akizukies.Orders;
using CapStore.Infrastructure.Html.Orders;

namespace CapStore.Infrastructure.Html.Tests.Orders;

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
        string html = await File.ReadAllTextAsync("../../../../CapStore.Infrastructure.Html.Tests/Orders/Assets/orders.html", Encoding.GetEncoding("SHIFT_JIS"));
        IEnumerable<IOrder> orders = await _repository.Fetch(html);

        IEnumerable<string> catalogs = orders
                                        .SelectMany(x => x.Components.Select(y => y.CatalogId.Value)
                                        .Distinct())
                                        .ToList();
        await File.WriteAllLinesAsync("../../../../CapStore.Infrastructure.Html.Tests/Orders/Assets/order-catalogs.txt", catalogs);

        Assert.True(html.Any());
        Assert.True(orders.Any());
        Assert.Equal(15, orders.Count());
    }
}
