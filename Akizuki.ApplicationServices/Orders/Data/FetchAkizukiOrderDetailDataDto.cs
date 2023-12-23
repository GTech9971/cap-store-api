using System.Text.Json.Serialization;
using Akizuki.Domain.Orders;

namespace Akizuki.ApplicationServices;

/// <summary>
/// 秋月電子の注文詳細のデータ
/// </summary>
public class FetchAkizukiOrderDetailDataDto
{

    public FetchAkizukiOrderDetailDataDto(IOrderDetail from)
    {
        OrderId = from.OrderId.Value;
        SlipNumber = from is OrderDetail
                        ? from.SlipNumber.Value
                        : 0;
        OrderDate = from.OrderDate.ToString();
        Components = from.Components.Select(x => new AkizukiOrderDetailComponentData(x));
    }

    [JsonPropertyName("orderId")]
    public string OrderId { get; }

    [JsonPropertyName("slipNumber")]
    public Int64 SlipNumber { get; }

    [JsonPropertyName("orderDate")]
    public string OrderDate { get; }

    [JsonPropertyName("components")]
    public IEnumerable<AkizukiOrderDetailComponentData> Components { get; }
}
