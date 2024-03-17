using System;
using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Tests.Akizukies.Catalogs
{
    public class AkizukiCatalogPageUrlTest
    {

        private const string CATEGORY = "秋月電子カタログページURL";

        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestNullException()
        {
            CatalogId catalogId = null!;
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new AkizukiCatalogPageUrl(catalogId);
            });

        }


        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("https://akizukidenshi.com/catalog/top.aspx")]
        [InlineData("https://akizukidenshi.com/catalog/e/enewall_dT")]
        public void TestExceptions(string url)
        {
            Assert.Throws<ValidationArgumentException>(() =>
            {
                new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            });
        }

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("https://akizukidenshi.com/catalog/g/g118177/", "118177")]
        [InlineData("https://akizukidenshi.com/catalog/g/g117091/", "117091")]
        [InlineData("https://akizukidenshi.com/catalog/g/g102724/", "102724")]
        [InlineData("https://akizukidenshi.com/catalog/g/g125103/", "125103")]
        [InlineData("https://akizukidenshi.com/catalog/g/g118150/", "118150")]
        public void TestSuccess(string url, string catalogId)
        {
            AkizukiCatalogPageUrl akizukiPageUrl = new AkizukiCatalogPageUrl(new AkizukiPageUrl(url));
            Assert.Equal(url, akizukiPageUrl.Value);
            Assert.Equal(akizukiPageUrl.CatalogId.Value, catalogId);

            AkizukiCatalogPageUrl urlFromCatalogId = new AkizukiCatalogPageUrl(new CatalogId(catalogId));
            Assert.Equal(url, urlFromCatalogId.Value);
        }
    }
}

