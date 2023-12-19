using System;
using Akizuki.Domain.Catalogs;
using Akizuki.Infrastructure.Catalogs.Html;

namespace Akizuki.Infrastructure.Html.Catalogs.Test
{
    public class AkizukPageHtmlRepositoryTest
    {
        private readonly AkizukiPageHtmlRepository _repository;

        public AkizukPageHtmlRepositoryTest()
        {
            _repository = new AkizukiPageHtmlRepository();
        }


        [Theory]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/gI-18177/")]
        public async Task TestSuccess(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("ＳｉＣショットキーバリアダイオード　６５０Ｖ６Ａ　ＴＲＳ６Ｅ６５Ｈ", akizukiPage.Component.Name.Value);
            Assert.Equal("TRS6E65H", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("株式会社東芝セミコンダクター社(TOSHIBA)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("半導体(モジュール)", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }

        [Theory]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/gM-18150/")]
        public async Task TestSuccessPICKit5(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("マイクロチップ　ＰＩＣｋｉｔ５", akizukiPage.Component.Name.Value);
            Assert.Equal("PG164150", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("マイコン関連", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }

        [Theory]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/gI-00252/")]
        public async Task TestSuccessPIC12F629(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("ＰＩＣマイコン　ＰＩＣ１２Ｆ６２９－Ｉ／Ｐ", akizukiPage.Component.Name.Value);
            Assert.Equal("PIC12F629-I/P", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("マイコン関連", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }

        [Theory]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/gI-04430/")]
        public async Task TestSuccessPIC16F1827(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("ＰＩＣマイコン　ＰＩＣ１６Ｆ１８２７－Ｉ／Ｐ", akizukiPage.Component.Name.Value);
            Assert.Equal("PIC16F1827-I/P", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("マイコン関連", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }


        [Theory]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/gK-10347/")]
        public async Task TestSuccessATMEGA(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("ＡＥ－ＡＴＭＥＧＡ３２８－ＭＩＮＩ　（Ａｒｄｕｉｎｏ　Ｐｒｏ　Ｍｉｎｉ上位互換）", akizukiPage.Component.Name.Value);
            Assert.Equal("AE-ATMEGA-328 MINI", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("株式会社秋月電子通商", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("マイコン関連", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }

        [Theory]
        [Trait("Category", "Akizuki")]
        [InlineData("https://akizukidenshi.com/catalog/g/gP-05294/")]
        public async Task TestSuccessBredBoard(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.Equal(url, akizukiPage.Url.Value);

            Assert.Equal("ブレッドボード　ＢＢ－８０１", akizukiPage.Component.Name.Value);
            Assert.Equal("BB-801", akizukiPage.Component.ModelName.Value);
            Assert.True(akizukiPage.Component.Description.Value.Any());
            Assert.Equal("Cixi Wanjie Electronic Co.,Ltd(慈渓市万捷電子有限公司)", akizukiPage.Component.Maker.Name.Value);
            Assert.Equal("パーツ一般", akizukiPage.Component.Category.Name.Value);
            Assert.True(akizukiPage.Component.Images.Any());
        }
    }
}

