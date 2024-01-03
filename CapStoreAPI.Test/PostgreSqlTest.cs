using Testcontainers.PostgreSql;
using Xunit;

namespace CapStoreAPI.Test;

/// <summary>
/// コントローラー層のテスト
/// <see cref="https://testcontainers.com/guides/testing-an-aspnet-core-web-app/"/>
/// </summary>
public sealed class PostgreSqlTest : IAsyncLifetime
{
    public readonly PostgreSqlContainer container = new PostgreSqlBuilder()
                .WithImage("cap-store-db:0.0.6")
                .WithDatabase("test_db")
                .WithUsername("test")
                .WithPassword("test")
                .WithPortBinding(5431, 5432)
                .WithCleanUp(true)
                .Build();

    public PostgreSqlTest() { }

    public async Task InitializeAsync()
    {
        await container.StartAsync();
    }

    public Task DisposeAsync()
    {
        return container.DisposeAsync().AsTask();
    }
}
