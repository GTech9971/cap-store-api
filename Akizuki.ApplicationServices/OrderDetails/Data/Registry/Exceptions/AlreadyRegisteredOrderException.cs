using Akizuki.Domain.Orders;

namespace Akizuki.ApplicationServices.Registry.Exceptions;

/// <summary>
/// 注文が既に登録済みの例外
/// </summary>
public class AlreadyRegisteredOrderException : Exception
{
    private readonly OrderId _orderId;

    public AlreadyRegisteredOrderException(OrderId orderId)
    : base($"オーダーが既に登録済みです ID:{orderId.Value}")
    {
        _orderId = orderId;
    }

    public OrderId OrderId => _orderId;
}
