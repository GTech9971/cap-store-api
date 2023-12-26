using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        string html =
            await File.ReadAllTextAsync("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/orders-detail.html",
            Encoding.GetEncoding("SHIFT_JIS"));

        AkizukiOrderDetailSource source = new AkizukiOrderDetailSource(html);

        IOrderDetail orderDetail = await _repository.Fetch(source);

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        string json = JsonSerializer.Serialize(orderDetail, options);

        Assert.True(html.Any());
        Assert.Equal("E230617-031873-01", orderDetail.OrderId.Value);
        Assert.Equal(569321486926, orderDetail.SlipNumber.Value);
        Assert.Equal("2023年6月17日", orderDetail.OrderDate.ToString());
        Assert.Equal(4, orderDetail.Components.Count());
    }
}
