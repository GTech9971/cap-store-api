using System;

namespace Akizuki.Domain.Catalogs.Test
{
	[TestClass]
	public class AkizukiPageUrlTest
    {

        private const string CATEGORY = "秋月電子ページURL";

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/catalog/top.aspx")]
        [DataRow("https://akizukidenshi.com/catalog/e/enewall_dT")]
        [DataRow("https://akizukidenshi.com/catalog/g/gI-18177/")]
        [DataRow("https://akizukidenshi.com/catalog/g/gI-17091/")]
        [DataRow("https://akizukidenshi.com/catalog/g/gP-02724/")]
        [DataRow("https://akizukidenshi.com/catalog/g/gR-25103/")]
        [DataRow("https://akizukidenshi.com/catalog/g/gM-18150/")]
        [DataRow("https://akizukidenshi.com/catalog/g/gM-18150/")]        
        public void TestSuccess(string url)
        {
            AkizukiPageUrl akizukiPageUrl = new AkizukiPageUrl(url);
            Assert.AreEqual(url, akizukiPageUrl.Value);
        }


        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("/img/goods/3/I-18177.jpg")]
        [DataRow("/img/goods/L/I-18177.jpg")]
        public void TestImageSuccess(string url)
        {
            AkizukiPageUrl akizukiPageUrl = new AkizukiPageUrl(url);            
        }
    }
}

