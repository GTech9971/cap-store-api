using System.Text;
using Akizuki.ApplicationServices;
using Akizuki.ApplicationServices.Data.Fetch;
using Akizuki.ApplicationServices.Data.Fetch.Response;
using Akizuki.ApplicationServices.OrderDetails;
using Akizuki.ApplicationServices.Registry;
using Akizuki.ApplicationServices.Registry.Exceptions;
using Akizuki.ApplicationServices.Registry.Request;
using Akizuki.Domain.Catalogs;
using Akizuki.Domain.Orders;
using Akizuki.Domain.Orders.Exceptions;
using Akizuki.Infrastructure.Html;
using CapStore.Domain.Components;
using CapStore.Domain.Inventories;
using Microsoft.AspNetCore.Mvc;

namespace cap_store_api;

[ApiController]
[Route("/api/v1/akizuki/orders")]
public class AkizukiOrderController
{
    private readonly OrderDetailApplicationService _orderDetailApplicationService;

    private readonly Encoding _encoding;

    public AkizukiOrderController(OrderDetailApplicationService orderDetailApplicationService)
    {
        _orderDetailApplicationService = orderDetailApplicationService;
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        _encoding = Encoding.GetEncoding("SHIFT_JIS");
    }

    /// <summary>
    /// 秋月電子の注文詳細ページのHtmlを解析して、注文詳細データを返す
    /// </summary>
    /// <param name="file">秋月電子の購入履歴詳細ページのHtmlファイル</param>
    /// <returns></returns>
    [HttpPost("details/upload")]
    public async Task<IActionResult> FetchOrderDetails(IFormFile file)
    {
        FetchAkizukiOrderDetailResponseData response;
        try
        {
            //ファイルチェック
            if (file.Length > AkizukiOrderDetailSource.MAX_SIZE_KB)
            {
                throw new OrderDetailSourceSizeException(file.Length);
            }

            //拡張子チェック
            if (Path.GetExtension(file.FileName).ToLowerInvariant() != ".html")
            {
                throw new OrderDetailSourceFormatException();
            }

            //ファイル読み取り
            string html;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (StreamReader reader = new StreamReader(file.OpenReadStream(), _encoding))
            {
                html = await reader.ReadToEndAsync();
            }

            AkizukiOrderDetailSource source = new AkizukiOrderDetailSource(html);
            FetchAkizukiOrderDetailDataDto data = await _orderDetailApplicationService.FetchAkizukiOrderDetailAsync(source);

            response = new FetchAkizukiOrderDetailSuccessResponseData(data);
        }
        catch (OrderDetailSourceSizeException ex)
        {
            response = new AKE0301Response(ex);
        }
        catch (OrderDetailSourceFormatException)
        {
            response = new AKE0302Response();
        }
        catch (AkizukiOrderDetailHtmlParseException)
        {
            response = new AKE0303Response();
        }


        JsonResult result = new JsonResult(response)
        {
            StatusCode = (int?)response.StatusCode
        };
        return result;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> RegistryOrder([FromBody] RegistryAkizukiOrderRequestData request)
    {
        RegistryAkizukiOrderResponseData response;

        try
        {
            IOrderDetail orderDetail = new OrderDetail(
                new OrderId(request.OrderId),
                new SlipNumber(request.SlipNumber),
                new OrderDate(DateOnly.Parse(request.OrderDate)),
                request.Components.Select(x => new AkizukiOrderComponent(
                    new Quantity(x.Quantity),
                    new Unit(x.Unit),
                    new CatalogId(x.CatalogId),
                    new ComponentId(x.ComponentId),
                    new ComponentName(x.ComponentName),
                    true
                ))
            );
            RegistryAkizukiOrderData data = await _orderDetailApplicationService.RegistryOrderAsync(orderDetail);
            response = new RegistryAkizukiOrderSuccessResponseData(data);
        }
        catch (AlreadyRegisteredOrderException ex)
        {
            response = new AKE0401Response(ex);
        }
        catch (NotRegisteredComponentIdException)
        {
            response = new AKE0402Response();
        }

        JsonResult result = new JsonResult(response)
        {
            StatusCode = (int?)response.StatusCode
        };
        return result;
    }

}
