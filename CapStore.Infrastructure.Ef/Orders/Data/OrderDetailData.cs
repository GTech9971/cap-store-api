using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Akizuki.Domain.Orders;
using CapStore.Infrastructure.Ef.Components.Data;

namespace CapStore.Infrastructure.Ef;

/// <summary>
/// efで使用する秋月電子の注文詳細データモデル
/// </summary>
[Table("order_details")]
public class OrderDetailData
{
    /// <summary>
    /// 主キー
    /// </summary>
    [Key]
    [Required]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// オーダーID
    /// 外部キー
    /// </summary>
    [Required]
    [Column("order_id")]
    public string OrderId { get; set; } = null!;


    [ForeignKey(nameof(OrderId))]
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
    [MaxLength(6)]
    [MinLength(6)]
    public string CatalogId { get; set; } = null!;


    /// <summary>
    /// 電子部品ID
    /// 外部キー
    /// </summary>
    [Required]
    [Column("component_id")]
    public int ComponentId { get; set; }


    [ForeignKey(nameof(ComponentId))]
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
