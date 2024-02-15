namespace Akizuki.Domain.Orders;

/// <summary>
/// 注文詳細に関するドメインサービス
/// </summary>
public class OrderDetailService
{

    private readonly IAkizukiOrderDetailRepository _repository;

    public OrderDetailService(IAkizukiOrderDetailRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 注文詳細が登録済みかどうか調べる
    /// </summary>
    /// <param name="orderDetail"></param>
    /// <returns></returns>
    public async Task<bool> Exists(IOrderDetail orderDetail)
    {
        IOrderDetail? found = await _repository.FetchAsync(orderDetail.OrderId);
        return found != null;
    }
}
