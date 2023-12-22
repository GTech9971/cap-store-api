using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Orders;

/// <summary>
/// 秋月電子で購入した注文詳細
/// </summary>
public class OrderDetail : IOrderDetail
{
    private readonly OrderId _orderId;
    private readonly SlipNumber _slipNumber;
    private readonly OrderDate _orderDate;
    private readonly IReadOnlyCollection<AkizukiOrderComponent> _components;

    public OrderDetail(OrderId orderId,
                        SlipNumber slipNumber,
                        OrderDate orderDate,
                        IEnumerable<AkizukiOrderComponent> components)
    {
        if (orderId == null)
        {
            throw new ValidationArgumentNullException("注文IDは必須です");
        }

        if (slipNumber == null)
        {
            throw new ValidationArgumentNullException("伝票番号は必須です");
        }

        if (orderDate == null)
        {
            throw new ValidationArgumentNullException("注文日は必須です");
        }

        if (components == null)
        {
            throw new ValidationArgumentNullException("注文内容は必須です");
        }

        if (components.Any() == false)
        {
            throw new ValidationArgumentException("注文内容が空です");
        }

        _orderId = orderId;
        _slipNumber = slipNumber;
        _orderDate = orderDate;
        _components = components.ToList().AsReadOnly();
    }

    /// <summary>
    /// 注文ID
    /// </summary>
    public OrderId OrderId => _orderId;
    /// <summary>
    /// 伝票番号
    /// </summary>
    public SlipNumber SlipNumber => _slipNumber;
    /// <summary>
    /// 注文日
    /// </summary>
    public OrderDate OrderDate => _orderDate;
    /// <summary>
    /// 注文内容詳細
    /// </summary>
    public IReadOnlyCollection<AkizukiOrderComponent> Components => _components;
}
