namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Fetch.Response;

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
