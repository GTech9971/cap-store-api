using Akizuki.Domain.Orders;
using Akizuki.Domain.Orders.Service;

namespace Akizuki.ApplicationServices;

/// <summary>
/// 秋月電子の注文詳細のアプリケーションサービス
/// </summary>
public class OrderDetailApplicationService
{
    private readonly IAkizukiOrderDetailRepository _repository;
    private readonly OrderDetailService _orderDetailService;

    public OrderDetailApplicationService(IAkizukiOrderDetailRepository repository,
                                            OrderDetailService orderDetailService)
    {
        _repository = repository;
        _orderDetailService = orderDetailService;
    }

    /// <summary>
    /// 秋月電子の注文詳細データから注文詳細を取得する
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public async Task<FetchAkizukiOrderDetailDataDto> FetchAkizukiOrderDetailAsync(string source)
    {
        IOrderDetail orderDetail = await _repository.Fetch(source);
        IOrderDetail applyIdOrderDetail = await _orderDetailService.ApplyComponentIdAsync(orderDetail);
        return new FetchAkizukiOrderDetailDataDto(applyIdOrderDetail);
    }

}
