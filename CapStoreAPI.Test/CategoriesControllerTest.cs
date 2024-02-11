using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CapStoreAPI.Test;

public class CategoriesControllerTest : IClassFixture<PostgreSqlTest>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;

    private readonly HttpClient _client;

    public CategoriesControllerTest(PostgreSqlTest fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        };
        _factory = new CustomWebApplicationFactory(fixture);
        _client = _factory.CreateClient(clientOptions);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    [Fact(DisplayName = "カテゴリー全取得")]
    [Trait("Controller", "Categories")]
    public async Task TestFetchAllCategories()
    {
        using HttpResponseMessage response = await _client.GetAsync("/api/v1/categories/");
        var jsonResponse = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(jsonResponse.Any());
    }
}
