using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CapStoreAPI.Test;

public class AkizukiControllerTest : IClassFixture<PostgreSqlTest>, IDisposable
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;

    public AkizukiControllerTest(PostgreSqlTest fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        };
        _webApplicationFactory = new CustomWebApplicationFactory(fixture);
        _httpClient = _webApplicationFactory.CreateClient(clientOptions);
    }

    public void Dispose()
    {
        _webApplicationFactory.Dispose();
    }

    [Theory(DisplayName = "電子部品取得API")]
    [Trait("Controller", "Akizuki")]
    [InlineData("I-18237")]
    public async Task FetchComponentsFromCatalogId(string catalogId)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync($"/api/v1/akizuki/catalogs/{catalogId}");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
