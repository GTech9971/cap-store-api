using CapStore.Infrastructure.Ef;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CapStoreAPI.Test;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
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
