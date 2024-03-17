using System;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Makers.Test
{
    public class MakerNameTest
    {
        private const string CATEGORY = "メーカー名";

        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestNullExceptions()
        {
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new MakerName(null!);
            });
        }

        [Fact]
        [Trait("Category", CATEGORY)]
        public void TestWhiteSpaceExceptions()
        {
            Assert.Throws<ValidationArgumentNullException>(() =>
            {
                new MakerName("");
            });
        }

        [Theory]
        [Trait("Category", CATEGORY)]
        [InlineData("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)")]
        [InlineData("Sipeed")]
        [InlineData("株式会社村田製作所(muRata)")]
        [InlineData("PARA LIGHT ELECTRONICS CO., LTD.")]
        [InlineData("株式会社東芝セミコンダクター社(TOSHIBA)")]
        public void TestSuccess(string name)
        {
            var makerName = new MakerName(name);
            Assert.Equal(name, makerName.Value);
        }
    }
}

