using System;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Shareds.Test
{
    public class ImageUrlTest
    {

        private const string CATEGORY = "画像URL";

        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestNullExceptions()
        {
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new ImageUrl("");
            });
        }

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("aaa")]
        [InlineData("http:/aas")]
        public void TestFormatExceptions(string url)
        {
            Assert.Throws<ValidationArgumentException>(() =>
            {
                new ImageUrl(url);
            });
        }

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("https://akizukidenshi.com/img/goods/S/M-18150.jpg")]
        [InlineData("https://akizukidenshi.com/img/goods/S/I-18269.jpg")]
        [InlineData("https://akizukidenshi.com/img/goods/L/M-18150.jpg")]
        [InlineData("https://akizukidenshi.com/img/goods/3/M-18150.jpg")]
        [InlineData("https://akizukidenshi.com/img/goods/L/I-04430.jpg")]
        [InlineData("https://akizukidenshi.com/img/goods/2/I-04430.jpg")]
        public void TestSuccess(string url)
        {
            var imageUrl = new ImageUrl(url);
            Assert.Equal(url, imageUrl.Value);
        }
    }
}

