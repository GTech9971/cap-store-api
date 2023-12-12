using Akizuki.Domain.Catalogs;
using Akizuki.Infrastructure.Catalogs.Html;
using cap_store_api.Filters;
using CapStore.Domain.Categories;
using CapStore.Domain.Components;
using CapStore.Domain.Makers;
using CapStore.Infrastructure.Ef;
using CapStore.Infrastructure.Ef.Categories;
using CapStore.Infrastructure.Ef.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//DB setting
builder.Services.AddDbContext<CapStoreDbContext>((_, options) =>
{
    //options.UseNpgsql(connectionString);
    //以下に書き換えないとコマンドが実行できない
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("CapStoreAPI"));
});


//DI
builder.Services.AddTransient<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddTransient<IMakerRepository, EfMakerRepository>();
builder.Services.AddTransient<IComponentRepository, EfComponentRepository>();
builder.Services.AddTransient<IAzikzukiPageRepository, AkizukiPageHtmlRepository>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new CapExceptionFilter());
});

var app = builder.Build();

//マイグレーション実行
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<CapStoreDbContext>())
{
    await context.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// テスト実施用に追加
/// <see cref="https://zenn.dev/shimat/articles/372d16077ecdf6"/>
/// </summary>
public partial class Program
{
}