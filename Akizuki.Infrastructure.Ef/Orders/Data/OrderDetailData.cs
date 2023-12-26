using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Akizuki.Domain.Orders;
using CapStore.Infrastructure.Ef.Components.Data;

namespace Akizuki.Infrastructure.Ef;

/// <summary>
/// efで使用する秋月電子の注文詳細データモデル
/// </summary>
[Table("order_details")]
public class OrderDetailData
{
    /// <summary>
    /// オーダーID
    /// 外部キー
    /// </summary>
    [Required]
    [ForeignKey(nameof(OrderData))]
    [Column("order_id")]
    public string OrderId { get; set; } = null!;

    public OrderData OrderData { get; set; } = null!;

    /// <summary>
    /// 数量
    /// </summary>
    [Required]
    [Column("quantity")]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    /// <summary>
    /// 在庫の単位
    /// </summary>
    [Required]
    [Column("unit")]
    public string Unit { get; set; } = null!;

    /// <summary>
    /// カタログID
    /// </summary>
    [Required]
    [Column("catalog_id")]
    [MaxLength(7)]
    [MinLength(7)]
    public string CatalogId { get; set; } = null!;


    /// <summary>
    /// 電子部品ID
    /// 外部キー
    /// </summary>
    [Required]
    [ForeignKey(nameof(Component))]
    [Column("component_id")]
    public int ComponentId { get; set; }

    public ComponentData Component { get; set; } = null!;



    public OrderDetailData() { }

    public OrderDetailData(OrderId orderId, AkizukiOrderComponent from)
    {
        OrderId = orderId.Value;
        Quantity = from.Quantity.Value;
        Unit = from.Unit.Value;
        CatalogId = from.CatalogId.Value;
        ComponentId = from.ComponentId.Value;
    }
}
