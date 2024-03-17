using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Components;

namespace CapStore.Domains.Akizukies.Orders;

/// <summary>
/// 秋月電子の注文詳細の永続化に関する操作を行う
/// </summary>
public interface IAkizukiOrderDetailRepository
{
    Task<IOrderDetail?> FetchAsync(OrderId orderId);

    Task<ComponentId?> FetchComponentIdAsync(CatalogId catalogId);

    Task SaveAsync(IOrderDetail orderDetail);
}
