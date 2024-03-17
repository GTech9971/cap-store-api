using System.Net;
using CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Registry.Exceptions;
using CapStore.Domains.Shareds;

namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Registry.Response;

public class AKE0401Response : RegistryAkizukiOrderResponseData
{
    public AKE0401Response(AlreadyRegisteredOrderException ex) : base(null)
    {
        Success = false;
        Errors = new List<Error>()
        {
            new Error(new ErrorCode("AKE0401"), new ErrorMessage(ex.Message))
        };
        StatusCode = HttpStatusCode.BadRequest;
    }
}
