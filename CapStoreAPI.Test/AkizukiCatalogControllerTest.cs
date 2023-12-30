using System.Net;
using System.Text;
using Akizuki.Domain.Catalogs;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace CapStoreAPI.Test;

public class AkizukiCatalogControllerTest : IClassFixture<PostgreSqlTest>, IDisposable
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;

    public AkizukiCatalogControllerTest(PostgreSqlTest fixture)
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

    // [Theory(DisplayName = "電子部品登録json作成")]
    // [Trait("Category", "Akizuki")]
    // public async Task CreateComponentsJson()
    // {
    //     IAsyncEnumerable<CatalogId> catalogIds = CreateCatalogIdsAsync();

    //     IAsyncEnumerable<string> jsonList = catalogIds.SelectAwait(async x =>
    //        {
    //            using HttpResponseMessage response = await _httpClient.GetAsync($"/api/v1/akizuki/catalogs/{x.Value}");
    //            return await response.Content.ReadAsStringAsync();
    //        });

    //     const string PATH = "../../../../CapStoreAPI.Test/Assets/components.json";
    //     using (StreamWriter writer = new StreamWriter(PATH, false, Encoding.UTF8))
    //     {
    //         await foreach (string json in jsonList)
    //         {
    //             await writer.WriteLineAsync(json);
    //         }
    //         await writer.FlushAsync();
    //     }
    // }

    [Theory(DisplayName = "電子部品登録json作成")]
    [Trait("Category", "Akizuki")]
    [MemberData(nameof(CreateCatalogIds))]
    public async Task CreateComponentsJson(string catalogIdStr)
    {
        CatalogId catalogId = new CatalogId(catalogIdStr);
        using HttpResponseMessage response = await _httpClient.GetAsync($"/api/v1/akizuki/catalogs/{catalogId.Value}");
        string json = await response.Content.ReadAsStringAsync();


        Assert.True(HttpStatusCode.OK == response.StatusCode
            || response.StatusCode == HttpStatusCode.NotFound);
    }

    public static IEnumerable<object[]> CreateCatalogIds()
    {
        const string PATH = "../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/order-catalogs.txt";
        using (StreamReader reader = new StreamReader(PATH, Encoding.UTF8))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) { continue; }
                yield return new object[] { line };
            }
        }
    }
}
