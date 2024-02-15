using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Akizuki.Domain.Catalogs;
using Akizuki.Domain.Orders;
using CapStore.Domain.Components;
using CapStore.Domain.Inventories;
using Microsoft.EntityFrameworkCore;

namespace CapStore.Infrastructure.Ef;

/// <summary>
/// efで使用する秋月電子の注文データモデル
/// </summary>
[Table("orders")]
[Index(nameof(OrderId))]
public class OrderData
{
    /// <summary>
    /// オーダーID
    /// 主キー
    /// </summary>
    [Key]
    [Required]
    [Column("order_id")]
    [MaxLength(17)]
    public string OrderId { get; set; } = null!;

    /// <summary>
    /// 注文日
    /// </summary>
    [Required]
    [Column("order_date")]
    [DataType(DataType.Date)]
    public DateOnly OrderDate { get; set; }

    /// <summary>
    /// 注文詳細
    /// </summary>
    public ICollection<OrderDetailData> OrderDetailDatas { get; set; } = null!;

    public OrderData() { }

    public OrderData(IOrderDetail from)
    {
        OrderId = from.OrderId.Value;
        OrderDate = from.OrderDate.Value;
        OrderDetailDatas = from.Components.Select(x => new OrderDetailData(from.OrderId, x)).ToList();
    }

    public IOrderDetail ToModel()
    {
        IEnumerable<AkizukiOrderComponent> components = OrderDetailDatas.Select(x => new AkizukiOrderComponent(
                    new Quantity(x.Quantity),
                    new Unit(x.Unit),
                    new CatalogId(x.CatalogId),
                    new ComponentId(x.ComponentId)
                ));

        return new OrderDetail(
            new OrderId(OrderId),
            new OrderDate(OrderDate),
           components
        );
    }
}
