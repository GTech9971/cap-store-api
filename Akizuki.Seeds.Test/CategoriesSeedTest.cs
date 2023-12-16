
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
    [InlineData("https://akizukidenshi.com/catalog/")]
    public async Task SuccessTest(string url)
    {
        AkizukiPageUrl akizukiPageUrl = new AkizukiPageUrl(url);
        IEnumerable<CategoryName> categoryNames = await _seed.FetchCategoryNamesFromAkizukiPage(akizukiPageUrl);

        Assert.True(categoryNames.Any());
    }


    [Theory(DisplayName = "カテゴリー保存")]
    [Trait("Category", "Seeds")]
    [InlineData("https://akizukidenshi.com/catalog/")]
    public async Task SaveSuccessTest(string url)
    {
        AkizukiPageUrl akizukiPageUrl = new AkizukiPageUrl(url);
        string path = await _seed.SaveAsync(akizukiPageUrl);

        Assert.True(File.Exists(path));
    }

    [Fact(DisplayName = "カテゴリー取得(text)")]
    [Trait("Category", "Seeds")]
    public async Task FetchSuccessTest()
    {
        List<CategoryName> categoryNames = await _seed.FetchCategoriesFromTxtAsync();

        Assert.True(categoryNames.Any());
    }
}
