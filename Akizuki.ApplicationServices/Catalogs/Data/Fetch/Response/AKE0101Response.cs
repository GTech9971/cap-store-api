using System.Net;
using Akizuki.ApplicationService.Catalogs;
using CapStore.Domain.Shareds;

namespace Akizuki.ApplicationServices;

public class AKE0101Response : FetchAkizukiPageErrorResponseDataDto
{
    public AKE0101Response(Exception exception)
    : base(new Error(
        new ErrorCode("AKE0101"), new ErrorMessage(exception.Message)
    ))
    {
        StatusCode = HttpStatusCode.NotFound;
    }
}
