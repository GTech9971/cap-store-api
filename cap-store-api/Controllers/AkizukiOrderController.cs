using System.Text;
using Akizuki.ApplicationServices;
using Akizuki.ApplicationServices.Data.Fetch;
using Akizuki.ApplicationServices.Data.Fetch.Response;
using Akizuki.ApplicationServices.OrderDetails;
using Akizuki.Domain.Orders;
using Akizuki.Domain.Orders.Exceptions;
using Akizuki.Infrastructure.Html;
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
    [HttpPost("details")]
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
}
