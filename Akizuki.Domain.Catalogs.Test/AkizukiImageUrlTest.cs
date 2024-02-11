using System;
namespace Akizuki.Domain.Catalogs.Test
{
    [TestClass]
    public class AkizukiImageUrlTest
    {
        private const string CATEGORY = "秋月電子ページURL";

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/img/goods/L/118177.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/1/118177.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/3/118177.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/C/111229.jpg")]
        public void TestSuccess(string url)
        {
            AkizukiImageUrl akizukiImageUrl = new AkizukiImageUrl(new AkizukiPageUrl(url));
            Assert.AreEqual(url, akizukiImageUrl.Value);
        }
    }
}

