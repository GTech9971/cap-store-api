using System.Text.Json.Serialization;
using Akizuki.Domain.Orders;

namespace Akizuki.ApplicationServices.Data.Fetch;

/// <summary>
/// 秋月電子の注文詳細のデータ
/// </summary>
public class FetchAkizukiOrderDetailDataDto
{

    public FetchAkizukiOrderDetailDataDto(IOrderDetail from)
    {
        OrderId = from.OrderId.Value;
        OrderDate = from.OrderDate.ToString();
        Components = from.Components.Select(x => new AkizukiOrderDetailComponentData(x));
    }

    [JsonPropertyName("orderId")]
    public string OrderId { get; }

    [JsonPropertyName("orderDate")]
    public string OrderDate { get; }

    [JsonPropertyName("components")]
    public IEnumerable<AkizukiOrderDetailComponentData> Components { get; }
}
