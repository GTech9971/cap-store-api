using System.Net;

namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Registry.Response;

/// <summary>
/// 注文登録成功レスポンス
/// </summary>
public class RegistryAkizukiOrderSuccessResponseData : RegistryAkizukiOrderResponseData
{
    public RegistryAkizukiOrderSuccessResponseData(RegistryAkizukiOrderData data)
    : base(data)
    {
        Success = true;
        StatusCode = HttpStatusCode.OK;
    }
}
