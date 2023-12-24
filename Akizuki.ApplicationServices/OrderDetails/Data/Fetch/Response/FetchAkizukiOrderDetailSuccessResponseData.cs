using Akizuki.ApplicationServices.Data.Fetch;
using Akizuki.ApplicationServices.Data.Fetch.Response;

namespace Akizuki.ApplicationServices;

/// <summary>
/// 秋月電子の注文詳細取得成功レスポンス
/// </summary>
public class FetchAkizukiOrderDetailSuccessResponseData
: FetchAkizukiOrderDetailResponseData
{
    public FetchAkizukiOrderDetailSuccessResponseData(FetchAkizukiOrderDetailDataDto data)
    : base(data)
    {
        Success = true;
        StatusCode = System.Net.HttpStatusCode.OK;
    }
}
