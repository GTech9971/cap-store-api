using System;
using CapStore.Infrastructure.Ef;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;
using System.Net;

namespace CapStoreAPI.Test
{
    /// <summary>
    /// コントローラー層のテスト
    /// <see cref="https://testcontainers.com/guides/testing-an-aspnet-core-web-app/"/>
    /// </summary>
    public class ComponentControllerTest : IAsyncLifetime
    {
        public readonly PostgreSqlContainer CONTAINER = new PostgreSqlBuilder()
            .WithImage("postgres")
            .WithDatabase("test_db")
            .WithUsername("test")
            .WithPassword("test")
            .WithCleanUp(true)
            .Build();


        public async Task InitializeAsync()
        {
            await CONTAINER.StartAsync();

            //テーブル再作成
            using (var context = CreateContext())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            }
        }

        public Task DisposeAsync()
        {
            return CONTAINER.DisposeAsync().AsTask();
        }

        /// <summary>
        /// DbContext作成
        /// </summary>
        /// <returns></returns>
        protected CapStoreDbContext CreateContext()
        {
            return new CapStoreDbContext(
            new DbContextOptionsBuilder<CapStoreDbContext>()
                .UseNpgsql(CONTAINER.GetConnectionString())
                .Options);
        }

        /// <summary>
        /// 登録テスト
        /// </summary>
        public sealed class RegistryTest : IClassFixture<ComponentControllerTest>, IDisposable
        {
            private readonly WebApplicationFactory<Program> _webApplicationFactory;

            private readonly HttpClient _httpClient;

            public RegistryTest(ComponentControllerTest fixture)
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


            [Fact]
            public async Task RegistryNotFoundTest()
            {
                using StringContent jsonContent = new(
                                   JsonSerializer.Serialize(new
                                   {
                                       name = "PIC16F1827",
                                       modelName = "PIC16f1827",
                                       description = "PICの説明",
                                       categoryId = 0,
                                       makerId = 0
                                   }),
                                   Encoding.UTF8,
                                   "application/json");

                using HttpResponseMessage response = await _httpClient.PostAsync(
                    "/api/v1/components/",
                    jsonContent);



                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");

                // Assert
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }


            private sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
            {
                private readonly string _connectionString;

                public CustomWebApplicationFactory(ComponentControllerTest fixture)
                {
                    _connectionString = fixture.CONTAINER.GetConnectionString();
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
}