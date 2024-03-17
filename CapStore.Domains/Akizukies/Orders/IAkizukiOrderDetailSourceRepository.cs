namespace CapStore.Domains.Akizukies.Orders;

/// <summary>
/// 秋月電子の注文詳細のデータソースに関する永続化を行う
/// </summary>
public interface IAkizukiOrderDetailSourceRepository
{
    Task<IOrderDetail> Fetch(AkizukiOrderDetailSource source);
}
