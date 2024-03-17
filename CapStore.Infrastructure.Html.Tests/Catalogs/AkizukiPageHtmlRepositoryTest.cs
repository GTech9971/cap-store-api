using System;
using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Infrastructure.Html.Catalogs;

namespace CapStore.Infrastructure.Html.Tests.Catalogs
{
    public class AkizukiPageHtmlRepositoryTest
    {
        private readonly AkizukiPageHtmlRepository _repository;

        public AkizukiPageHtmlRepositoryTest()
        {
            _repository = new AkizukiPageHtmlRepository();
        }


        [Theory(DisplayName = "秋月電子カタログ情報取得")]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/g118177/")]
        public async Task TestSuccess(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("SiCショットキーバリアダイオード 650V6A TRS6E65H", akizukiPage.Component.Name.Value);
            Assert.Equal("TRS6E65H", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("株式会社東芝セミコンダクター社(TOSHIBA)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("半導体", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }

        [Theory(DisplayName = "秋月電子カタログ情報取得")]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/g118150/")]
        public async Task TestSuccessPICKit5(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("マイクロチップ PICkit5", akizukiPage.Component.Name.Value);
            Assert.Equal("PG164150", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("開発ツール・ボード", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }

        [Theory(DisplayName = "秋月電子カタログ情報取得")]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/g100252/")]
        public async Task TestSuccessPIC12F629(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("PICマイコン PIC12F629-I/P", akizukiPage.Component.Name.Value);
            Assert.Equal("PIC12F629-I/P", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("半導体", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }

        [Theory(DisplayName = "秋月電子カタログ情報取得")]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/g104430/")]
        public async Task TestSuccessPIC16F1827(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("PICマイコン PIC16F1827-I/P", akizukiPage.Component.Name.Value);
            Assert.Equal("PIC16F1827-I/P", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("半導体", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }


        [Theory(DisplayName = "秋月電子カタログ情報取得")]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/g110347/")]
        public async Task TestSuccessATMEGA(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("AE-ATMEGA328-MINI (Arduino Pro Mini上位互換)", akizukiPage.Component.Name.Value);
            Assert.Equal("AE-ATMEGA-328 MINI", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("株式会社秋月電子通商", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("開発ツール・ボード", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }

        [Theory(DisplayName = "秋月電子カタログ情報取得")]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/g105294/")]
        public async Task TestSuccessBredBoard(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("ブレッドボード BB-801", akizukiPage.Component.Name.Value);
            Assert.Equal("BB-801", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("Cixi Wanjie Electronic Co.,Ltd(慈渓市万捷電子有限公司)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("基板・ブレッドボード・ラグ板", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }
    }
}

