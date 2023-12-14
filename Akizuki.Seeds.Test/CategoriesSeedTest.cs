
using Akizuki.Domain.Catalogs;
using CapStore.Domain.Categories;

namespace Akizuki.Seeds.Test;

public class CategoriesSeedTest
{
    private readonly CategoriesSeed _seed;

    public CategoriesSeedTest()
    {
        _seed = new CategoriesSeed();
    }

    [Theory(DisplayName = "カテゴリー取得")]
    [Trait("Category", "Seeds")]
    [InlineData("https://akizukidenshi.com/catalog/c/c/")]
    public async Task SuccessTest(string url)
    {
        AkizukiPageUrl akizukiPageUrl = new AkizukiPageUrl(url);
        IEnumerable<CategoryName> categoryNames = await _seed.FetchCategoryNamesFromAkizukiPage(akizukiPageUrl);

        Assert.True(categoryNames.Any());
        Assert.Equal(24, categoryNames.Count());
    }
}
