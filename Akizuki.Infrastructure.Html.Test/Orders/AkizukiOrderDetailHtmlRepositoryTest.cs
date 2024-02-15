using System.Text;
using Akizuki.Domain.Orders;

namespace Akizuki.Infrastructure.Html.Test;

public class AkizukiOrderDetailHtmlRepositoryTest
{
    private readonly AkizukiOrderDetailHtmlRepository _repository;

    public AkizukiOrderDetailHtmlRepositoryTest()
    {
        _repository = new AkizukiOrderDetailHtmlRepository();
    }

    [Fact(DisplayName = "注文詳細情報取成功テスト")]
    [Trait("Category", "Akizuki")]
    public async Task SuccessTest()
    {
        string current = Directory.GetCurrentDirectory();
        string html = await File.ReadAllTextAsync("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/order-detail.html", Encoding.UTF8);

        AkizukiOrderDetailSource source = new AkizukiOrderDetailSource(html);

        IOrderDetail orderDetail = await _repository.Fetch(source);

        Assert.True(html.Any());
        Assert.Equal("E230617-031873-01", orderDetail.OrderId.Value);
        Assert.Equal("2023年6月17日", orderDetail.OrderDate.ToString());
        Assert.Equal(4, orderDetail.Components.Count());

        AkizukiOrderComponent first = orderDetail.Components.First();
        AkizukiOrderComponent second = orderDetail.Components.Skip(1).First();
        AkizukiOrderComponent third = orderDetail.Components.Skip(2).First();
        AkizukiOrderComponent last = orderDetail.Components.Last();

        Assert.Equal("101306", first.CatalogId.Value);
        Assert.Equal(10, first.Quantity.Value);

        Assert.Equal("105779", second.CatalogId.Value);
        Assert.Equal(5, second.Quantity.Value);

        Assert.Equal("109862", third.CatalogId.Value);
        Assert.Equal(10, third.Quantity.Value);

        Assert.Equal("104430", last.CatalogId.Value);
        Assert.Equal(1, last.Quantity.Value);
    }
}
