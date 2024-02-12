using System.Net;
using CapStore.ApplicationServices.Categories.Data;
using CapStore.Domain.Categories;
using CapStore.Domain.Shareds.Responses;
using Microsoft.AspNetCore.Mvc;

namespace cap_store_api;

[ApiController]
[Route("/api/v1/categories")]
public class CategoriesController
{
    private readonly ICategoryRepository _repository;
    public CategoriesController(ICategoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        BaseResponse<FetchCategoryDataDto> response = new BaseResponse<FetchCategoryDataDto>()
        {
            Success = true,
            Data = await _repository
                        .FetchAll()
                        .Select(x => new FetchCategoryDataDto(x))
                        .ToListAsync()
        };

        JsonResult result = new JsonResult(response)
        {
            StatusCode = (int)(response.Data.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent)
        };

        return result;
    }
}
