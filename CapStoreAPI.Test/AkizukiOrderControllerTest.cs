using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Fetch;
using CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Registry.Request;
using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Akizukies.Orders;
using CapStore.Domains.Components;
using CapStore.Domains.Inventories;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CapStoreAPI.Test;

public class AkizukiOrderControllerTest : IClassFixture<PostgreSqlTest>, IDisposable
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;

    public AkizukiOrderControllerTest(PostgreSqlTest fixture)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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

    [Fact(DisplayName = "注文詳細取得API")]
    [Trait("Controller", "Akizuki")]
    public async Task TestFetchOrderDetailSuccess()
    {
        FileInfo file = new FileInfo("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/orders-detail.html");
        string html = await File.ReadAllTextAsync(file.FullName, Encoding.GetEncoding("SHIFT_JIS"));
        byte[] data = Encoding.GetEncoding("SHIFT_JIS").GetBytes(html);

        MultipartFormDataContent httpContent = new MultipartFormDataContent();
        ByteArrayContent fileContent = new ByteArrayContent(data);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

        httpContent.Add(fileContent, "file", file.Name);


        using HttpResponseMessage response = await _httpClient.PostAsync($"/api/v1/akizuki/orders/details/upload", httpContent);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact(DisplayName = "注文詳細取得解析失敗API")]
    [Trait("Controller", "Akizuki")]
    public async Task TestFetchOrderDetailParseFail()
    {
        FileInfo file = new FileInfo("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/orders.html");
        string html = await File.ReadAllTextAsync(file.FullName, Encoding.GetEncoding("SHIFT_JIS"));
        byte[] data = Encoding.GetEncoding("SHIFT_JIS").GetBytes(html);

        MultipartFormDataContent httpContent = new MultipartFormDataContent();
        ByteArrayContent fileContent = new ByteArrayContent(data);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

        httpContent.Add(fileContent, "file", file.Name);


        using HttpResponseMessage response = await _httpClient.PostAsync($"/api/v1/akizuki/orders/details/upload", httpContent);

        string jsonResponse = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Contains("AKE0303", jsonResponse);
    }

    [Fact(DisplayName = "注文詳細取得APIソースファイル拡張子失敗")]
    [Trait("Controller", "Akizuki")]
    public async Task TestFetchOrderDetailSourceExtension()
    {
        FileInfo file = new FileInfo("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/orders-detail.html");
        string html = await File.ReadAllTextAsync(file.FullName, Encoding.GetEncoding("SHIFT_JIS"));
        byte[] data = Encoding.GetEncoding("SHIFT_JIS").GetBytes(html);

        MultipartFormDataContent httpContent = new MultipartFormDataContent();
        ByteArrayContent fileContent = new ByteArrayContent(data);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

        httpContent.Add(fileContent, "file", file.Name + ".txt");


        using HttpResponseMessage response = await _httpClient.PostAsync($"/api/v1/akizuki/orders/details/upload", httpContent);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("AKE0302", jsonResponse);
    }

    [Fact(DisplayName = "注文詳細取得APIソースファイルサイズ失敗")]
    [Trait("Controller", "Akizuki")]
    public async Task TestFetchOrderDetailSourceSizeExtension()
    {
        FileInfo file = new FileInfo("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/orders-detail.html");
        string html = await File.ReadAllTextAsync(file.FullName, Encoding.GetEncoding("SHIFT_JIS"));
        for (int i = 0; i < 10; i++)
        {
            html += html;
        }
        byte[] data = Encoding.GetEncoding("SHIFT_JIS").GetBytes(html);

        MultipartFormDataContent httpContent = new MultipartFormDataContent();
        ByteArrayContent fileContent = new ByteArrayContent(data);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

        httpContent.Add(fileContent, "file", file.Name + ".txt");


        using HttpResponseMessage response = await _httpClient.PostAsync($"/api/v1/akizuki/orders/details/upload", httpContent);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("AKE0301", jsonResponse);
    }

    [Fact(DisplayName = "注文登録成功テスト")]
    [Trait("Controller", "Akizuki")]
    public async Task RegistryOrderDetailSuccessTest()
    {

        RegistryAkizukiOrderRequestData request = new RegistryAkizukiOrderRequestData()
        {
            OrderId = "E230617-031873-01",
            SlipNumber = 569321486926,
            OrderDate = "2023-06-19",
            Components = new List<AkizukiOrderDetailComponentData>()
            {
                new AkizukiOrderDetailComponentData(new AkizukiOrderComponent(
                    new Quantity(10),
                    new Unit("個"),
                    new CatalogId("101306"),
                    new ComponentId(5)
                )),
                new AkizukiOrderDetailComponentData(new AkizukiOrderComponent(
                    new Quantity(5),
                    new Unit("個"),
                    new CatalogId("105779"),
                    new ComponentId(32)
                ))
            }
        };
        using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        using HttpResponseMessage response = await _httpClient.PostAsync("/api/v1/akizuki/orders", jsonContent);

        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
