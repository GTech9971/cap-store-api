using System;
using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Catalogs.Test
{
	[TestClass]
	public class AkizukiCatalogPageUrlTest
	{

		private const string CATEGORY = "秋月電子カタログページURL";

		[TestMethod]
		[TestCategory(CATEGORY)]
		[ExpectedException(typeof(ValidationArgumentNullException))]
		public void TestNullException()
		{
			new AkizukiCatalogPageUrl(null);
		}


        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentException))]        
        [DataRow("https://akizukidenshi.com/catalog/top.aspx")]
        [DataRow("https://akizukidenshi.com/catalog/e/enewall_dT")]
        public void TestExceptions(string url)
        {
            new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/catalog/g/gI-18177/", "I-18177")]
        [DataRow("https://akizukidenshi.com/catalog/g/gI-17091/", "I-17091")]
        [DataRow("https://akizukidenshi.com/catalog/g/gP-02724/", "P-02724")]
        [DataRow("https://akizukidenshi.com/catalog/g/gR-25103/", "R-25103")]
        [DataRow("https://akizukidenshi.com/catalog/g/gM-18150/", "M-18150")]
        public void TestSuccess(string url, string catalogId)
        {
            AkizukiCatalogPageUrl akizukiPageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            Assert.AreEqual(url, akizukiPageUrl.Value);
            Assert.AreEqual(akizukiPageUrl.CatalogId.Value, catalogId);
        }
    }
}

