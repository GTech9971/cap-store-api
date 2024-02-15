using Akizuki.Domain.Catalogs;
using CapStore.Domain.Components;

namespace Akizuki.Domain.Orders;

/// <summary>
/// 秋月電子の注文詳細の永続化に関する操作を行う
/// </summary>
public interface IAkizukiOrderDetailRepository
{
    Task<IOrderDetail?> FetchAsync(OrderId orderId);

    Task<ComponentId?> FetchComponentIdAsync(CatalogId catalogId);

    Task SaveAsync(IOrderDetail orderDetail);
}
