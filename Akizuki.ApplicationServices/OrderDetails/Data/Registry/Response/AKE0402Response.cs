using System.Net;
using CapStore.Domain.Shareds;

namespace Akizuki.ApplicationServices;

public class AKE0402Response : RegistryAkizukiOrderResponseData
{
    public AKE0402Response()
    : base(null)
    {
        Success = false;
        Errors = new List<Error>()
        {
            new Error(new ErrorCode("AKE0402"), new ErrorMessage("電子部品IDがマスターに登録されていません"))
        };

        StatusCode = HttpStatusCode.NotFound;
    }
}
