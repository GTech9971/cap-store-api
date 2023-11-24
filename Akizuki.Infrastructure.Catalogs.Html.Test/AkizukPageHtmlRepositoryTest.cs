using System;
using Akizuki.Domain.Catalogs;

namespace Akizuki.Infrastructure.Catalogs.Html.Test
{
	[TestClass]
	public class AkizukPageHtmlRepositoryTest
	{

		private const string CATEGORY = "秋月電子ページレポジトリ";

		private AkizukiPageHtmlRepository _repository;

		[TestInitialize]
		public void Setup()
		{
			_repository = new AkizukiPageHtmlRepository();
		}


		[TestMethod]
		[TestCategory(CATEGORY)]
		[DataRow("https://akizukidenshi.com/catalog/g/gI-18177/")]
		public async Task TestSuccess(string url)
		{			
			AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
			AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
			Assert.AreEqual(url, akizukiPage.Url.Value);

			Assert.AreEqual("ＳｉＣショットキーバリアダイオード　６５０Ｖ６Ａ　ＴＲＳ６Ｅ６５Ｈ", akizukiPage.Component.Name.Value);
            Assert.AreEqual("TRS6E65H", akizukiPage.Component.ModelName.Value);
			Assert.IsTrue(akizukiPage.Component.Description.Value.Any());
            Assert.AreEqual("株式会社東芝セミコンダクター社(TOSHIBA)", akizukiPage.Component.Maker.Name.Value);
            Assert.AreEqual("半導体(モジュール)", akizukiPage.Component.Category.Name.Value);
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/catalog/g/gM-18150/")]
        public async Task TestSuccessPICKit5(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.AreEqual(url, akizukiPage.Url.Value);

            Assert.AreEqual("マイクロチップ　ＰＩＣｋｉｔ５", akizukiPage.Component.Name.Value);
            Assert.AreEqual("PG164150", akizukiPage.Component.ModelName.Value);
            Assert.IsTrue(akizukiPage.Component.Description.Value.Any());
            Assert.AreEqual("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", akizukiPage.Component.Maker.Name.Value);
            Assert.AreEqual("マイコン関連", akizukiPage.Component.Category.Name.Value);
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/catalog/g/gI-00252/")]
        public async Task TestSuccessPIC12F629(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.AreEqual(url, akizukiPage.Url.Value);

            Assert.AreEqual("ＰＩＣマイコン　ＰＩＣ１２Ｆ６２９－Ｉ／Ｐ", akizukiPage.Component.Name.Value);
            Assert.AreEqual("PIC12F629-I/P", akizukiPage.Component.ModelName.Value);
            Assert.IsTrue(akizukiPage.Component.Description.Value.Any());
            Assert.AreEqual("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", akizukiPage.Component.Maker.Name.Value);
            Assert.AreEqual("マイコン関連", akizukiPage.Component.Category.Name.Value);
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/catalog/g/gI-04430/")]
        public async Task TestSuccessPIC16F1827(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.AreEqual(url, akizukiPage.Url.Value);

            Assert.AreEqual("ＰＩＣマイコン　ＰＩＣ１６Ｆ１８２７－Ｉ／Ｐ", akizukiPage.Component.Name.Value);
            Assert.AreEqual("PIC16F1827-I/P", akizukiPage.Component.ModelName.Value);
            Assert.IsTrue(akizukiPage.Component.Description.Value.Any());
            Assert.AreEqual("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", akizukiPage.Component.Maker.Name.Value);
            Assert.AreEqual("マイコン関連", akizukiPage.Component.Category.Name.Value);
        }


        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/catalog/g/gK-10347/")]
        public async Task TestSuccessATMEGA(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.AreEqual(url, akizukiPage.Url.Value);

            Assert.AreEqual("ＡＥ－ＡＴＭＥＧＡ３２８－ＭＩＮＩ　（Ａｒｄｕｉｎｏ　Ｐｒｏ　Ｍｉｎｉ上位互換）", akizukiPage.Component.Name.Value);
            Assert.AreEqual("AE-ATMEGA-328 MINI", akizukiPage.Component.ModelName.Value);
            Assert.IsTrue(akizukiPage.Component.Description.Value.Any());
            Assert.AreEqual("株式会社秋月電子通商", akizukiPage.Component.Maker.Name.Value);
            Assert.AreEqual("マイコン関連", akizukiPage.Component.Category.Name.Value);
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/catalog/g/gP-05294/")]
        public async Task TestSuccessBredBoard(string url)
        {
            AkizukiCatalogPageUrl pageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(pageUrl);
            Assert.AreEqual(url, akizukiPage.Url.Value);

            Assert.AreEqual("ブレッドボード　ＢＢ－８０１", akizukiPage.Component.Name.Value);
            Assert.AreEqual("BB-801", akizukiPage.Component.ModelName.Value);
            Assert.IsTrue(akizukiPage.Component.Description.Value.Any());
            Assert.AreEqual("Cixi Wanjie Electronic Co.,Ltd(慈渓市万捷電子有限公司)", akizukiPage.Component.Maker.Name.Value);
            Assert.AreEqual("パーツ一般", akizukiPage.Component.Category.Name.Value);
        }
    }
}

