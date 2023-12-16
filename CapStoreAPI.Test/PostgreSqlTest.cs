using Akizuki.Seeds;
using CapStore.Domain.Categories;
using CapStore.Domain.Makers;
using CapStore.Infrastructure.Ef;
using CapStore.Infrastructure.Ef.Categories.Data;
using CapStore.Infrastructure.Ef.Makers.Data;
using Microsoft.EntityFrameworkCore;
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
              .WithImage("postgres")
              .WithDatabase("test_db")
              .WithUsername("test")
              .WithPassword("test")
              .WithCleanUp(true)
              .Build();

    private readonly CategoriesSeed _categoriesSeed;
    private readonly MakersSeed _makersSeed;

    public PostgreSqlTest()
    {
        _categoriesSeed = new CategoriesSeed();
        _makersSeed = new MakersSeed();
    }

    public async Task InitializeAsync()
    {
        await container.StartAsync();

        //テーブル再作成
        using (var context = CreateCapStoreDbContext())
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            //カテゴリー名の追加
            IEnumerable<CategoryData> categories =
             (await _categoriesSeed.FetchCategoriesFromTxtAsync())
                .Select(x => new Category(CategoryId.UnDetectId(), x, null))
                .Select(x => new CategoryData(x));
            await context.CategoryDatas.AddRangeAsync(categories);

            //メーカー名の追加
            IEnumerable<MakerData> makers =
            (await _makersSeed.FetchMakerFromTxtAsync())
                .Select(x => new Maker(MakerId.UnDetect(), x, null))
                .Select(x => new MakerData(x));
            await context.MakerDatas.AddRangeAsync(makers);

            //コミット
            await context.SaveChangesAsync();
        }
    }

    public Task DisposeAsync()
    {
        return container.DisposeAsync().AsTask();
    }

    /// <summary>
    /// DbContext作成
    /// </summary>
    /// <returns></returns>
    private CapStoreDbContext CreateCapStoreDbContext()
    {
        return new CapStoreDbContext(
        new DbContextOptionsBuilder<CapStoreDbContext>()
            .UseNpgsql(container.GetConnectionString())
            .Options);
    }
}
