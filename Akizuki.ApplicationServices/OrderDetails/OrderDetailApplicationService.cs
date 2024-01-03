using Akizuki.ApplicationServices.Data.Fetch;
using Akizuki.ApplicationServices.Registry;
using Akizuki.ApplicationServices.Registry.Exceptions;
using Akizuki.Domain.Orders;
using CapStore.Domain.Components;
using CapStore.Domain.Components.Services;
using System.Linq;

namespace Akizuki.ApplicationServices.OrderDetails;

/// <summary>
/// 秋月電子の注文詳細のアプリケーションサービス
/// </summary>
public class OrderDetailApplicationService
{
    private readonly IAkizukiOrderDetailRepository _repository;
    private readonly IAkizukiOrderDetailSourceRepository _orderDetailSourceRepository;
    private readonly IComponentRepository _componentRepository;

    private readonly OrderDetailService _orderDetailService;
    private readonly ComponentService _componentService;


    public OrderDetailApplicationService(IAkizukiOrderDetailRepository repository,
                                            IAkizukiOrderDetailSourceRepository orderDetailSourceRepository,
                                            IComponentRepository componentRepository,
                                            OrderDetailService orderDetailService,
                                            ComponentService componentService)
    {
        _repository = repository;
        _orderDetailSourceRepository = orderDetailSourceRepository;
        _componentRepository = componentRepository;
        _orderDetailService = orderDetailService;
        _componentService = componentService;
    }

    /// <summary>
    /// 秋月電子の注文詳細データから注文詳細を取得する
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="AkizukiOrderDetailHtmlParseException"></exception>
    /// <returns></returns>
    public async Task<FetchAkizukiOrderDetailDataDto> FetchAkizukiOrderDetailAsync(AkizukiOrderDetailSource source)
    {

        IOrderDetail orderDetail = await _orderDetailSourceRepository.Fetch(source);
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

    /// <summary>
    /// 注文を登録する
    /// </summary>
    /// <param name="orderDetail"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyRegisteredOrderException"></exception>
    /// <exception cref="NotRegisteredComponentIdException"></exception>
    public async Task<RegistryAkizukiOrderData> RegistryOrderAsync(IOrderDetail orderDetail)
    {
        //注文の2重登録チェック
        if (await _orderDetailService.Exists(orderDetail))
        {
            throw new AlreadyRegisteredOrderException(orderDetail.OrderId);
        }

        //ComponentId登録確認
        IEnumerable<ComponentId> idList = orderDetail.Components.Select(x => x.ComponentId);

        if (await _componentService.ExistsAll(idList) == false)
        {
            throw new NotRegisteredComponentIdException();
        }

        await _repository.Save(orderDetail);

        return new RegistryAkizukiOrderData();
    }

}
