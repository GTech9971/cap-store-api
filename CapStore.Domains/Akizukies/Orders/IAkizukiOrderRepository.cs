namespace CapStore.Domains.Akizukies.Orders;

/// <summary>
/// 秋月電子の注文の永続化に関する操作を行う
/// </summary>
public interface IAkizukiOrderRepository
{
    Task<IEnumerable<IOrder>> Fetch(string source);
}
