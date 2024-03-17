using CapStore.Domains.Akizukies.Orders.Exceptions;
using CapStore.Domains.Shareds;

namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Fetch.Response;

public class AKE0301Response : FetchAkizukiOrderDetailResponseData
{
    public AKE0301Response(OrderDetailSourceSizeException exception) : base(null)
    {
        Success = false;
        Errors = new List<Error>(1)
        {
            new Error(new ErrorCode("AKE0301"),
                    new ErrorMessage(exception.Message))
        };

        StatusCode = System.Net.HttpStatusCode.BadRequest;
    }
}