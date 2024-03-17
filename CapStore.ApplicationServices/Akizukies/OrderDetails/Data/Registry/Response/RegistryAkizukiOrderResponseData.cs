using CapStore.Domains.Shareds.Responses;

namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Registry.Response;

/// <summary>
/// 注文登録レスポンス
/// </summary>
public class RegistryAkizukiOrderResponseData : BaseResponse<RegistryAkizukiOrderData>
{
    public RegistryAkizukiOrderResponseData(RegistryAkizukiOrderData? data) : base()
    {
        Data = new List<RegistryAkizukiOrderData>() { data };
    }
}
