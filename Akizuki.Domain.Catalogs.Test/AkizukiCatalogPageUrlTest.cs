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
            CatalogId catalogId = null;
            new AkizukiCatalogPageUrl(catalogId);
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
        [DataRow("https://akizukidenshi.com/catalog/g/g118177/", "118177")]
        [DataRow("https://akizukidenshi.com/catalog/g/g117091/", "117091")]
        [DataRow("https://akizukidenshi.com/catalog/g/g102724/", "102724")]
        [DataRow("https://akizukidenshi.com/catalog/g/g125103/", "125103")]
        [DataRow("https://akizukidenshi.com/catalog/g/g118150/", "118150")]
        public void TestSuccess(string url, string catalogId)
        {
            AkizukiCatalogPageUrl akizukiPageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            Assert.AreEqual(url, akizukiPageUrl.Value);
            Assert.AreEqual(akizukiPageUrl.CatalogId.Value, catalogId);

            AkizukiCatalogPageUrl urlFromCatalogId = new AkizukiCatalogPageUrl(new CatalogId(catalogId));
            Assert.AreEqual(url, urlFromCatalogId.Value);
        }
    }
}

