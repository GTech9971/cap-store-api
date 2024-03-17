using System.Net;
using CapStore.Domains.Shareds;

namespace CapStore.ApplicationServices.Akizukies.Catalogs.Data.Fetch.Response;

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
