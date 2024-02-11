using CapStore.ApplicationServices.Makers.Data;
using CapStore.Domain.Makers;
using Microsoft.AspNetCore.Mvc;

namespace cap_store_api;

[ApiController]
[Route("/api/v1/makers")]
public class MakersController
{
    private readonly IMakerRepository _repository;

    public MakersController(IMakerRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IAsyncEnumerable<FetchMakerDataDto> FetchAll()
    {
        return _repository
                    .FetchAll()
                    .Select(x => new FetchMakerDataDto(x));
    }
}
