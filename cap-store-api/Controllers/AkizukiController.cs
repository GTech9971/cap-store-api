﻿using Akizuki.ApplicationService.Catalogs;
using Akizuki.ApplicationService.Catalogs.Data.Fetch;
using Akizuki.Infrastructure.Catalogs.Html;
using Microsoft.AspNetCore.Mvc;

namespace cap_store_api;

[ApiController]
[Route("/api/v1/akizuki/catalogs")]
public class AkizukiController
{
    private readonly CatalogApplicationService _applicationService;

    public AkizukiController(CatalogApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet("{catalogId}")]
    public async Task<IActionResult> FetchComponentFromAkizukiCatalog(string catalogId)
    {
        FetchAkizukiPageResponseDataDto response;
        try
        {
            FetchAkizukiPageDataDto data = await _applicationService.FetchComponentFromAkizukiCatalogIdAsync(catalogId);
            response = new FetchAkizukiPageSuccessResponseDataDto(data);
        }
        catch (AkizukiPageHtmlParseException)
        {
            response = new AKE0102Response();
        }

        JsonResult result = new JsonResult(response)
        {
            StatusCode = (int?)response.StatusCode
        };
        return result;
    }
}
