namespace Akizuki.Domain.Orders;

/// <summary>
/// 秋月電子の注文詳細の永続化に関する操作を行う
/// </summary>
public interface IAkizukiOrderDetailRepository
{
    Task<IOrderDetail> Fetch(AkizukiOrderDetailSource source);

    Task Save(IOrderDetail orderDetail);
}
