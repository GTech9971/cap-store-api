using CapStore.Domains.Shareds.Responses;

namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Fetch.Response;

/// <summary>
/// 秋月電子の注文詳細取得のレスポンスデータ
/// </summary>
public class FetchAkizukiOrderDetailResponseData : BaseResponse<FetchAkizukiOrderDetailDataDto>
{
    public FetchAkizukiOrderDetailResponseData(FetchAkizukiOrderDetailDataDto? data) : base()
    {
        if (data != null)
        {
            Data = new List<FetchAkizukiOrderDetailDataDto>(1) { data };
        }
    }
}
