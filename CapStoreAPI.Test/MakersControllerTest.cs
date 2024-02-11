using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CapStoreAPI.Test;

public class MakersControllerTest : IClassFixture<PostgreSqlTest>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _clint;

    public MakersControllerTest(PostgreSqlTest fixture)
    {
        var options = new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        };
        _factory = new CustomWebApplicationFactory(fixture);
        _clint = _factory.CreateClient(options);
    }

    public void Dispose()
    {
        _clint.Dispose();
    }

    [Fact(DisplayName = "全メーカー取得")]
    [Trait("Controller", "Makers")]
    public async Task TestFetchAllMaker()
    {
        using HttpResponseMessage response = await _clint.GetAsync("/api/v1/makers");
        var jsonResponse = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(jsonResponse.Any());
    }
}
