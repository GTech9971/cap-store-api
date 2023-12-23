using System.Text.Json.Serialization;
using Akizuki.Domain.Orders;

namespace Akizuki.ApplicationServices;

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
        ComponentName = from.ComponentName.Value;
    }

    [JsonPropertyName("quantity")]
    public int Quantity { get; }

    [JsonPropertyName("unit")]
    public string Unit { get; }

    [JsonPropertyName("catalogId")]
    public string CatalogId { get; }

    [JsonPropertyName("componentId")]
    public int ComponentId { get; }

    [JsonPropertyName("name")]
    public string ComponentName { get; }
}
