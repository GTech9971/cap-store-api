using CapStore.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace Akizuki.Infrastructure.Ef.Test;

public class BaseEfRepositoryTest : DbContext, IAsyncLifetime
{
    protected readonly PostgreSqlContainer _container;
    public BaseEfRepositoryTest()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres")
            .WithDatabase("test_db")
            .WithUsername("test")
            .WithPassword("test")
            .WithCleanUp(true)
            .Build();
    }

    public virtual async Task InitializeAsync()
    {
        await _container.StartAsync();
        //テーブル再作成
        using (var context = CreateCapStoreDbContext())
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        using (var context = CreateAkizukiDbContext())
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _container.DisposeAsync();
    }

    /// <summary>
    /// DbContext作成
    /// </summary>
    /// <returns></returns>
    protected AkizukiDbContext CreateAkizukiDbContext()
    {
        return new AkizukiDbContext(
        new DbContextOptionsBuilder<AkizukiDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options);
    }

    /// <summary>
    /// DbContext作成
    /// </summary>
    /// <returns></returns>
    protected CapStoreDbContext CreateCapStoreDbContext()
    {
        return new CapStoreDbContext(
        new DbContextOptionsBuilder<AkizukiDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options);
    }
}
