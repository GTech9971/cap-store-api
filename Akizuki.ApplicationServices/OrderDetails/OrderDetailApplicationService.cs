using Akizuki.ApplicationServices.Data.Fetch;
using Akizuki.Domain.Orders;
using CapStore.Domain.Components;

namespace Akizuki.ApplicationServices.OrderDetails;

/// <summary>
/// 秋月電子の注文詳細のアプリケーションサービス
/// </summary>
public class OrderDetailApplicationService
{
    private readonly IAkizukiOrderDetailRepository _repository;
    private readonly IComponentRepository _componentRepository;

    public OrderDetailApplicationService(IAkizukiOrderDetailRepository repository,
                                            IComponentRepository componentRepository)
    {
        _repository = repository;
        _componentRepository = componentRepository;
    }

    /// <summary>
    /// 秋月電子の注文詳細データから注文詳細を取得する
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="AkizukiOrderDetailHtmlParseException"></exception>
    /// <returns></returns>
    public async Task<FetchAkizukiOrderDetailDataDto> FetchAkizukiOrderDetailAsync(AkizukiOrderDetailSource source)
    {

        IOrderDetail orderDetail = await _repository.Fetch(source);
        //電子部品マスターに登録済みかどうか確認する
        List<AkizukiOrderComponent> applyRegisteredOrderComponents = await ApplyRegisteredOrderComponentsAsync(orderDetail.Components);


        IOrderDetail applyRegisteredOrderDetail = new OrderDetail(orderDetail.OrderId,
                                                                    orderDetail.SlipNumber,
                                                                    orderDetail.OrderDate,
                                                                    applyRegisteredOrderComponents);

        return new FetchAkizukiOrderDetailDataDto(applyRegisteredOrderDetail);
    }

    /// <summary>
    /// 電子部品マスターに登録済みか確認する。
    /// 登録済みであればIDを付与し直す
    /// </summary>
    /// <param name="components"></param>
    /// <returns></returns>
    private async Task<List<AkizukiOrderComponent>> ApplyRegisteredOrderComponentsAsync(IEnumerable<AkizukiOrderComponent> components)
    {
        List<AkizukiOrderComponent> list = new List<AkizukiOrderComponent>();
        foreach (AkizukiOrderComponent x in components)
        {
            Component? component = await _componentRepository.Fetch(x.ComponentName);
            ComponentId componentId = component == null
                                         ? x.ComponentId
                                         : component.Id;

            bool registered = component != null;
            list.Add(new AkizukiOrderComponent(x.Quantity, x.Unit, x.CatalogId, componentId, x.ComponentName, registered));
        }

        return list;
    }

}
