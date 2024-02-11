using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Akizuki.Domain.Catalogs;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CapStoreAPI.Test;

public class AkizukiCatalogControllerTest : IClassFixture<PostgreSqlTest>, IDisposable
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _options;

    public AkizukiCatalogControllerTest(PostgreSqlTest fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false,
        };
        _webApplicationFactory = new CustomWebApplicationFactory(fixture);
        _httpClient = _webApplicationFactory.CreateClient(clientOptions);

        //json日本語変換有効化
        _options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
    }

    public void Dispose()
    {
        _webApplicationFactory.Dispose();
    }

    [Theory(DisplayName = "電子部品取得API")]
    [Trait("Controller", "Akizuki")]
    [InlineData("118237")]
    public async Task FetchComponentsFromCatalogId(string catalogId)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync($"/api/v1/akizuki/catalogs/{catalogId}");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact(DisplayName = "電子部品登録json作成")]
    [Trait("Category", "Akizuki")]
    public async Task CreateComponentsJson()
    {
        IEnumerable<CatalogId> catalogIds =
            CreateCatalogIds()
            .ToList()
            .Select(x => x.First().ToString()!)
            .Select(x => new CatalogId(x));

        List<string> jsonList = new List<string>(catalogIds.Count());
        foreach (CatalogId catalogId in catalogIds)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/v1/akizuki/catalogs/{catalogId.Value}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                jsonList.Add(json);
            }
        }

        IEnumerable<RegistryComponent> documents = jsonList
            .Select(x => JsonSerializer.Deserialize<ComponentData>(x))
            .Where(x => x != null)
            .Select(x => new RegistryComponent(x!))
            .ToList();

        IEnumerable<long> makerIdList = documents.Select(x => x.makerId).ToList();


        foreach (RegistryComponent registryComponent in documents)
        {
            string path = $"../../../../CapStoreAPI.Test/Assets/RegistryComponents/{registryComponent.name}.json";
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                string json = JsonSerializer.Serialize(registryComponent, _options);
                await writer.WriteLineAsync(json);
                await writer.FlushAsync();
            }
        }
    }

    [Theory(DisplayName = "カタログIDリスト成功テスト")]
    [Trait("Category", "Akizuki")]
    [MemberData(nameof(CreateCatalogIds))]
    public async Task TestCatalogIdListSuccess(string catalogIdStr)
    {
        CatalogId catalogId = new CatalogId(catalogIdStr);
        using HttpResponseMessage response = await _httpClient.GetAsync($"/api/v1/akizuki/catalogs/{catalogId.Value}");
        string json = await response.Content.ReadAsStringAsync();


        Assert.True(HttpStatusCode.OK == response.StatusCode
            || response.StatusCode == HttpStatusCode.NotFound);
    }

    /// <summary>
    /// カタログIDリスト
    /// </summary>
    /// <returns></returns>
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
    public class RegistryComponent
    {
        public string name { get; set; }

        public string modelName { get; set; }

        public string description { get; set; }

        public long categoryId { get; set; }

        public long makerId { get; set; }

        public IEnumerable<string> images { get; set; }

        public RegistryComponent(ComponentData data)
        {
            name = data.Data[0].Name;
            modelName = data.Data[0].ModelName;
            description = data.Data[0].Description;
            categoryId = data.Data[0].Category.Id;
            makerId = data.Data[0].Maker.Id;
            images = data.Data[0].Images.Select(x => x.ToString());
        }
    }

    public partial class ComponentData
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public Datum[] Data { get; set; } = null!;

        [JsonPropertyName("errors")]
        public object Errors { get; set; } = null!;
    }

    public partial class Datum
    {
        [JsonPropertyName("componentId")]
        public long ComponentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("modelName")]
        public string ModelName { get; set; } = null!;

        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [JsonPropertyName("category")]
        public Category Category { get; set; } = null!;

        [JsonPropertyName("maker")]
        public Category Maker { get; set; } = null!;

        [JsonPropertyName("images")]
        public Uri[] Images { get; set; } = null!;
    }

    public partial class Category
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("image")]
        public object Image { get; set; } = null!;
    }
}
