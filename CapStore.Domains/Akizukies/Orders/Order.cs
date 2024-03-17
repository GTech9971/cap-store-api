using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Akizukies.Orders;

/// <summary>
/// 秋月電子の注文モデル
/// </summary>
public class Order : IOrder
{
    private readonly OrderId _orderId;
    private readonly OrderDate _orderDate;
    private readonly IReadOnlyList<AkizukiComponent> _components;

    public Order(OrderId orderId,
                OrderDate orderDate,
                IEnumerable<AkizukiComponent> components)
    {
        if (orderId == null)
        {
            throw new ValidationArgumentNullException("注文IDは必須です");
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
        _orderDate = orderDate;
        _components = components.ToList().AsReadOnly();
    }

    /// <summary>
    /// 注文ID
    /// </summary>
    public OrderId OrderId => _orderId;

    /// <summary>
    /// 注文日
    /// </summary>
    public OrderDate OrderDate => _orderDate;

    /// <summary>
    /// 注文内容
    /// </summary>
    public IReadOnlyList<AkizukiComponent> Components => _components;
}
