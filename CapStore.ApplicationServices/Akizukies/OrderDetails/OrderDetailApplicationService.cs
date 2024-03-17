using CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Fetch;
using CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Registry;
using CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Registry.Exceptions;
using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Akizukies.Orders;
using CapStore.Domains.Akizukies.Orders.Services;
using CapStore.Domains.Components;
using CapStore.Domains.Components.Services;

namespace CapStore.ApplicationServices.Akizukies.OrderDetails;

/// <summary>
/// 秋月電子の注文詳細のアプリケーションサービス
/// </summary>
public class OrderDetailApplicationService
{
    private readonly IAkizukiOrderDetailRepository _repository;
    private readonly IAkizukiOrderDetailSourceRepository _orderDetailSourceRepository;
    private readonly IAkizukiPageRepository _akizukiPageRepository;
    private readonly IComponentRepository _componentRepository;

    private readonly OrderDetailService _orderDetailService;
    private readonly ComponentService _componentService;


    public OrderDetailApplicationService(IAkizukiOrderDetailRepository repository,
                                            IAkizukiOrderDetailSourceRepository orderDetailSourceRepository,
                                            IAkizukiPageRepository akizukiPageRepository,
                                            IComponentRepository componentRepository,
                                            OrderDetailService orderDetailService,
                                            ComponentService componentService)
    {
        _repository = repository;
        _orderDetailSourceRepository = orderDetailSourceRepository;
        _akizukiPageRepository = akizukiPageRepository;
        _componentRepository = componentRepository;
        _orderDetailService = orderDetailService;
        _componentService = componentService;
    }

    /// <summary>
    /// 秋月電子の注文詳細データから注文詳細を取得する
    /// カタログIDの電子部品データが電子部品マスターに登録済みか確認する。
    /// 登録済みであれば電子部品IDを付与。未登録であれば、電子部品マスターに登録を行い電子部品IDを付与
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="AkizukiOrderDetailHtmlParseException"></exception>
    /// <returns></returns>
    public async Task<FetchAkizukiOrderDetailDataDto> FetchAkizukiOrderDetailAsync(AkizukiOrderDetailSource source)
    {
        IOrderDetail orderDetail = await _orderDetailSourceRepository.Fetch(source);
        //電子部品マスターに登録済みかどうか確認する
        IEnumerable<AkizukiOrderComponent> applyRegisteredOrderComponents = await Task.WhenAll(orderDetail.Components.Select(async x =>
        {
            ComponentId? componentId = await _repository.FetchComponentIdAsync(x.CatalogId);
            if (componentId != null)
            {
                return x.ApplyNewComponentId(componentId);
            }
            //カタログIDをもとに電子部品情報を取得
            AkizukiPage akizukiPage = await _akizukiPageRepository.FetchAkizukiPageAsync(x.CatalogId);
            Component registeredComponent = await _componentRepository.Save(akizukiPage.Component);
            return x.ApplyNewComponentId(registeredComponent.Id);
        }));

        IOrderDetail applyRegisteredOrderDetail = new OrderDetail(orderDetail.OrderId,
                                                                    orderDetail.OrderDate,
                                                                    applyRegisteredOrderComponents);

        return new FetchAkizukiOrderDetailDataDto(applyRegisteredOrderDetail);
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

        await _repository.SaveAsync(orderDetail);

        return new RegistryAkizukiOrderData();
    }

}
