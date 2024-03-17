using System;
using CapStore.Domains.Akizukies.Catalogs;

namespace CapStore.Domains.Tests.Akizukies.Catalogs
{
    public class AkizukiPageUrlTest
    {

        private const string CATEGORY = "秋月電子ページURL";

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("https://akizukidenshi.com/catalog/top.aspx")]
        [InlineData("https://akizukidenshi.com/catalog/e/enewall_dT")]
        [InlineData("https://akizukidenshi.com/catalog/g/gI-18177/")]
        [InlineData("https://akizukidenshi.com/catalog/g/gI-17091/")]
        [InlineData("https://akizukidenshi.com/catalog/g/gP-02724/")]
        [InlineData("https://akizukidenshi.com/catalog/g/gR-25103/")]
        [InlineData("https://akizukidenshi.com/catalog/g/gM-18150/")]
        public void TestSuccess(string url)
        {
            AkizukiPageUrl akizukiPageUrl = new AkizukiPageUrl(url);
            Assert.Equal(url, akizukiPageUrl.Value);
        }


        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("/img/goods/3/I-18177.jpg")]
        [InlineData("/img/goods/L/I-18177.jpg")]
        public void TestImageSuccess(string url)
        {
            AkizukiPageUrl akizukiPageUrl = new AkizukiPageUrl(url);
        }
    }
}

