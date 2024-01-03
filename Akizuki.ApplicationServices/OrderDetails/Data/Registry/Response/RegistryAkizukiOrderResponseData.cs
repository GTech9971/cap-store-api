using Akizuki.ApplicationServices.Registry;
using CapStore.Domain.Shareds.Responses;

namespace Akizuki.ApplicationServices;

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
