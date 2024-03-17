using System.Text.Json.Serialization;
using CapStore.Domains.Akizukies.Orders;

namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Fetch;

/// <summary>
///  秋月電子注文詳細内容のデータ
/// </summary>
public class AkizukiOrderDetailComponentData
{

    public AkizukiOrderDetailComponentData(AkizukiOrderComponent from)
    {
        Quantity = from.Quantity.Value;
        Unit = from.Unit.Value;
        CatalogId = from.CatalogId.Value;
        ComponentId = from.ComponentId.Value;
    }

    public AkizukiOrderDetailComponentData() { }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; } = null!;

    [JsonPropertyName("catalogId")]
    public string CatalogId { get; set; } = null!;

    [JsonPropertyName("componentId")]
    public int ComponentId { get; set; }
}
