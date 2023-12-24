using System.Net;
using Akizuki.ApplicationServices.Data.Fetch.Response;
using CapStore.Domain.Shareds;

namespace Akizuki.ApplicationServices;

public class AKE0303Response : FetchAkizukiOrderDetailResponseData
{
    public AKE0303Response() : base(null)
    {
        Success = false;
        Errors = new List<Error>()
        {
            new Error(
                new ErrorCode("AKE0303"),
                new ErrorMessage("Htmlファイルの解析に失敗しました")
            )
        };
        StatusCode = HttpStatusCode.InternalServerError;
    }
}
