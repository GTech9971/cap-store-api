using System.Net;
using CapStore.Domains.Shareds;

namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Registry.Response;

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
