using CapStore.ApplicationServices.Categories.Data;
using CapStore.Domain.Categories;
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
    public IAsyncEnumerable<FetchCategoryDataDto> FetchAll()
    {
        return _repository
            .FetchAll()
            .Select(x => new FetchCategoryDataDto(x));
    }
}
