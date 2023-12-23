using CapStore.Domain.Components;

namespace Akizuki.Domain.Orders.Service;

/// <summary>
/// 秋月電子の注文詳細に関するドメインサービス
/// </summary>
public class OrderDetailService
{

    private readonly IComponentRepository _componentRepository;

    public OrderDetailService(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }

    /// <summary>
    /// 注文詳細の電子部品名から電子部品IDを取得して、付与した状態で返す
    /// </summary>
    /// <param name="orderDetail"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundComponentException"></exception>
    public async Task<OrderDetail> ApplyComponentIdAsync(IOrderDetail orderDetail)
    {
        IEnumerable<AkizukiOrderComponent> list = await Task.WhenAll(orderDetail.Components.Select(async x =>
              {
                  Component? component = await _componentRepository.Fetch(x.ComponentName);
                  if (component == null)
                  {
                      throw new NotFoundComponentException(x.ComponentName);
                  }
                  AkizukiOrderComponent applyId =
                   new AkizukiOrderComponent(x.Quantity, x.Unit, x.CatalogId, component.Id, x.ComponentName);
                  return applyId;
              }));

        return new OrderDetail(orderDetail.OrderId, orderDetail.SlipNumber, orderDetail.OrderDate, list);
    }
}
