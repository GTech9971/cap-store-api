using System.Text.Encodings.Web;
using System.Text.Unicode;

using cap_store_api.Filters;
using CapStore.ApplicationServices.Akizukies.Catalogs;
using CapStore.ApplicationServices.Akizukies.OrderDetails;
using CapStore.ApplicationServices.Components;
using CapStore.Domains;
using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Akizukies.Orders;
using CapStore.Domains.Akizukies.Orders.Services;
using CapStore.Domains.Categories;
using CapStore.Domains.Components;
using CapStore.Domains.Components.Services;
using CapStore.Domains.Makers;
using CapStore.Infrastructure.Ef;
using CapStore.Infrastructure.Ef.Categories;
using CapStore.Infrastructure.Ef.Components;
using CapStore.Infrastructure.Html.Catalogs;
using CapStore.Infrastructure.Html.Orders;
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
//マスター
builder.Services.AddTransient<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddTransient<IMakerRepository, EfMakerRepository>();
builder.Services.AddTransient<IComponentRepository, EfComponentRepository>();
//
builder.Services.AddTransient<IAkizukiPageRepository, AkizukiPageHtmlRepository>();
//発注
builder.Services.AddTransient<IAkizukiOrderDetailSourceRepository, AkizukiOrderDetailHtmlRepository>();
builder.Services.AddTransient<IAkizukiOrderDetailRepository, EfOrderRepository>();

//services
builder.Services.AddScoped<ComponentService>();
builder.Services.AddScoped<ComponentsApplicationService>();
builder.Services.AddScoped<CatalogApplicationService>();
builder.Services.AddScoped<OrderDetailService>();
builder.Services.AddScoped<OrderDetailApplicationService>();
builder.Services.AddScoped(typeof(FilterSortService<>));

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new CapExceptionFilter());
}).AddJsonOptions(options =>
{
    //インデント有効
    options.JsonSerializerOptions.WriteIndented = true;
    //日本語有効か
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});

var app = builder.Build();

//マイグレーション実行
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<CapStoreDbContext>())
{
    if (await context.CategoryDatas.AnyAsync() == false)
    {
        await context.Database.MigrateAsync();
    }
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