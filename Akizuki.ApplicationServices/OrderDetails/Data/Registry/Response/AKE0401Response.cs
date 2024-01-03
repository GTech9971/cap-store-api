using System.Net;
using Akizuki.ApplicationServices.Registry.Exceptions;
using CapStore.Domain.Shareds;

namespace Akizuki.ApplicationServices;

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
