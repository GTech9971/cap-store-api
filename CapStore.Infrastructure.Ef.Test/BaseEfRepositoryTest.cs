using System;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace CapStore.Infrastructure.Ef.Test
{
    public class BaseEfRepositoryTest : IAsyncLifetime
    {
        protected readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
            .WithImage("cap-store-db:0.0.7")
            .WithPortBinding(5431, 5432)
            .WithDatabase("test_db")
            .WithUsername("test")
            .WithPassword("test")
            .WithCleanUp(true)
            .Build();

        public CapStoreDbContext _context;

        public async Task InitializeAsync()
        {
            //コンテナ起動
            await _container.StartAsync();
            _context = new CapStoreDbContext(
                        new DbContextOptionsBuilder<CapStoreDbContext>()
                        .UseNpgsql(_container.GetConnectionString())
                        .Options);
        }

        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
            await _container.DisposeAsync();
        }
    }
}

