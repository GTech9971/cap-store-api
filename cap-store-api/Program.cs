using Akizuki.Domain.Catalogs;
using Akizuki.Infrastructure.Catalogs.Html;
using CapStore.Domain.Categories;
using CapStore.Domain.Makers;
using CapStore.Infrastructure.Ef;
using CapStore.Infrastructure.Ef.Categories;
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
builder.Services.AddTransient<IAzikzukiPageRepository, AkizukiPageHtmlRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

