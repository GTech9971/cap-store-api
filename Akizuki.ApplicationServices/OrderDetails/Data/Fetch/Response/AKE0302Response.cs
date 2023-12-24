using System.Net;
using Akizuki.ApplicationServices.Data.Fetch.Response;
using CapStore.Domain.Shareds;

namespace Akizuki.ApplicationServices;

public class AKE0302Response : FetchAkizukiOrderDetailResponseData
{
    public AKE0302Response() : base(null)
    {
        Success = false;
        Errors = new List<Error>(1)
        {
            new Error(new ErrorCode("AKE0302"),
                        new ErrorMessage("Htmlファイルのフォーマットが不正です"))
        };
        StatusCode = HttpStatusCode.BadRequest;
    }
}
