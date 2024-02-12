using System.Net;
using CapStore.ApplicationServices.Makers.Data;
using CapStore.Domain.Makers;
using CapStore.Domain.Shareds.Responses;
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
    public async Task<IActionResult> FetchAll()
    {
        BaseResponse<FetchMakerDataDto> response = new BaseResponse<FetchMakerDataDto>()
        {
            Success = true,
            Data = await _repository
                    .FetchAll()
                    .Select(x => new FetchMakerDataDto(x))
                    .ToListAsync()
        };
        JsonResult result = new JsonResult(response)
        {
            StatusCode = (int)(response.Data.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent)
        };
        return result;
    }
}
