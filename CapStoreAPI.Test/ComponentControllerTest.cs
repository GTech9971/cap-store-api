using System;
using CapStore.Infrastructure.Ef;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;
using System.Net;

namespace CapStoreAPI.Test
{
    /// <summary>
    /// 登録テスト
    /// </summary>
    public sealed class RegistryTest : IClassFixture<PostgreSqlTest>, IDisposable
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;

        private readonly HttpClient _httpClient;

        public RegistryTest(PostgreSqlTest fixture)
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


        [Fact(DisplayName = "電子部品登録")]
        [Trait("Controller", "Component")]
        public async Task RegistryNotFoundTest()
        {
            using StringContent jsonContent = new(
                               JsonSerializer.Serialize(new
                               {
                                   name = "PIC16F1827",
                                   modelName = "PIC16f1827",
                                   description = "PICの説明",
                                   categoryId = 1,
                                   makerId = 1
                               }),
                               Encoding.UTF8,
                               "application/json");

            using HttpResponseMessage response = await _httpClient.PostAsync(
                "/api/v1/components/",
                jsonContent);



            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "電子部品取得API")]
        [Trait("Controller", "Component")]
        public async Task FetchComponentsEmptyTest()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/components/");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        private sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
        {
            private readonly string _connectionString;

            public CustomWebApplicationFactory(PostgreSqlTest fixture)
            {
                _connectionString = fixture.container.GetConnectionString();
            }

            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureServices(services =>
                {
                    services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<CapStoreDbContext>) == service.ServiceType));
                    services.AddDbContext<CapStoreDbContext>((_, option) => option.UseNpgsql(_connectionString));
                });
            }
        }
    }

}