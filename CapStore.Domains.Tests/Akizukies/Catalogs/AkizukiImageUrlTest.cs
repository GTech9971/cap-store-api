using System;
using CapStore.Domains.Akizukies.Catalogs;
namespace CapStore.Domains.Tests.Akizukies.Catalogs
{
    public class AkizukiImageUrlTest
    {
        private const string CATEGORY = "秋月電子ページURL";

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("https://akizukidenshi.com/img/goods/L/118177.jpg")]
        [InlineData("https://akizukidenshi.com/img/goods/1/118177.jpg")]
        [InlineData("https://akizukidenshi.com/img/goods/3/118177.jpg")]
        [InlineData("https://akizukidenshi.com/img/goods/C/111229.jpg")]
        public void TestSuccess(string url)
        {
            AkizukiImageUrl akizukiImageUrl = new AkizukiImageUrl(new AkizukiPageUrl(url));
            Assert.Equal(url, akizukiImageUrl.Value);
        }
    }
}

