using Akizuki.Domain.Catalogs;
using CapStore.Domain.Makers;

namespace Akizuki.Seeds.Test;

public class MakersSeedTest
{

    private readonly MakersSeed _seed;

    public MakersSeedTest()
    {
        _seed = new MakersSeed();
    }

    [Theory(DisplayName = "メーカー名取得")]
    [Trait("Category", "Seeds")]
    [InlineData("https://akizukidenshi.com/catalog/c/c/")]
    public async Task SuccessTest(string url)
    {
        AkizukiPageUrl akizukiPageUrl = new AkizukiPageUrl(url);
        IEnumerable<MakerName> makerNames = await _seed.FetchMakerNamesFromAkizukiPage(akizukiPageUrl);

        Assert.True(makerNames.Any());
    }

    [Theory(DisplayName = "メーカー名保存")]
    [Trait("Category", "Seeds")]
    [InlineData("https://akizukidenshi.com/catalog/c/c/")]
    public async Task SaveSuccessTest(string url)
    {
        AkizukiPageUrl akizukiPageUrl = new AkizukiPageUrl(url);
        string path = await _seed.SaveAsync(akizukiPageUrl);

        Assert.True(File.Exists(path));
    }
}
