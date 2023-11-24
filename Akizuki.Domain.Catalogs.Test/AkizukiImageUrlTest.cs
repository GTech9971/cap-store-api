using System;
namespace Akizuki.Domain.Catalogs.Test
{
	[TestClass]
	public class AkizukiImageUrlTest
	{
        private const string CATEGORY = "秋月電子ページURL";

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/img/goods/L/I-18177.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/1/I-18177.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/3/I-18177.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/C/B-11229.jpg")]
        public void TestSuccess(string url)
        {
            AkizukiImageUrl akizukiImageUrl = new AkizukiImageUrl(new AkizukiPageUrl(url));
            Assert.AreEqual(url, akizukiImageUrl.Value);
        }
    }
}

