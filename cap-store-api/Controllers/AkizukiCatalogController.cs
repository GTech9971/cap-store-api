using CapStore.ApplicationServices.Akizukies.Catalogs;
using CapStore.ApplicationServices.Akizukies.Catalogs.Data.Fetch;
using CapStore.ApplicationServices.Akizukies.Catalogs.Data.Fetch.Response;
using CapStore.Infrastructure.Html.Catalogs.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace cap_store_api;

[ApiController]
[Route("/api/v1/akizuki/catalogs")]
public class AkizukiCatalogController
{
    private readonly CatalogApplicationService _applicationService;

    public AkizukiCatalogController(CatalogApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet("{catalogId}")]
    public async Task<IActionResult> FetchComponentFromAkizukiCatalog(string catalogId)
    {
        FetchAkizukiPageResponseDataDto response;
        try
        {
            FetchAkizukiPageDataDto<FetchCategoryDataDto, FetchMakerDataDto> data = await _applicationService.FetchComponentFromAkizukiCatalogIdAsync(catalogId);
            response = new FetchAkizukiPageSuccessResponseDataDto(data);
        }
        catch (AkizukiPageHtmlParseException)
        {
            response = new AKE0102Response();
        }
        catch (AkizukiCatalogIdUnAvailableException ex)
        {
            response = new AKE0101Response(ex);
        }

        JsonResult result = new JsonResult(response)
        {
            StatusCode = (int?)response.StatusCode
        };
        return result;
    }
}
