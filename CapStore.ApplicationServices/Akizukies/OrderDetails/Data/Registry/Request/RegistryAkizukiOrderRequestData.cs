using System.Text.Json.Serialization;
using CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Fetch;

namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Registry.Request;

/// <summary>
/// 秋月電子注文登録リクエスト
/// </summary>
public class RegistryAkizukiOrderRequestData
{

    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = null!;

    [JsonPropertyName("slipNumber")]
    public Int64 SlipNumber { get; set; }

    [JsonPropertyName("orderDate")]
    public string OrderDate { get; set; } = null!;

    [JsonPropertyName("components")]
    public IEnumerable<AkizukiOrderDetailComponentData> Components { get; set; } = null!;

    public RegistryAkizukiOrderRequestData() { }
}
