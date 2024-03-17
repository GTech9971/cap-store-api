using System;
using CapStore.Domains.Shareds;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Makers.Test
{
    public class MakerTest
    {
        private const string CATEGORY = "メーカー";

        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestNullIdExceptions()
        {
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new Maker(null!, new MakerName("TEST"), new ImageUrl("https://akizukidenshi.com/img/goods/L/I-18167.jpg"));
            });
        }

        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestNullNameExceptions()
        {
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new Maker(new MakerId(0), null!, new ImageUrl("https://akizukidenshi.com/img/goods/L/I-18167.jpg"));
            });
        }

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData(0, "Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", "https://akizukidenshi.com/img/goods/3/I-18167.jpg")]
        [InlineData(1, "Sipeed", null)]
        [InlineData(2, "株式会社村田製作所(muRata)", null)]
        [InlineData(3, "PARA LIGHT ELECTRONICS CO., LTD.", null)]
        [InlineData(100, "株式会社東芝セミコンダクター社(TOSHIBA)", null)]
        public void TestSuccess(int id, string name, string url)
        {
            var makerId = new MakerId(id);
            var makerName = new MakerName(name);
            ImageUrl? image = null;
            if (string.IsNullOrWhiteSpace(url) == false)
            {
                image = new ImageUrl(url);
            }

            var maker = new Maker(makerId, makerName, image);
            Assert.Equal(id, maker.Id.Value);
            Assert.Equal(name, maker.Name.Value);
            Assert.Equal(url, maker.Image?.Value);
        }
    }
}

