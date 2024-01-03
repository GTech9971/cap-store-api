using System.Net;
using Akizuki.ApplicationServices.Registry;

namespace Akizuki.ApplicationServices;

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
