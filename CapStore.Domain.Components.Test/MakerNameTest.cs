using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components.Test
{
	[TestClass]
	public class MakerNameTest
	{
		private const string CATEGORY = "メーカー名";

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentNullException))]
        public void TestNullExceptions()
        {
            new MakerName(null!);
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentNullException))]
        public void TestWhiteSpaceExceptions()
        {
            new MakerName("");
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)")]
        [DataRow("Sipeed")]
        [DataRow("株式会社村田製作所(muRata)")]
        [DataRow("PARA LIGHT ELECTRONICS CO., LTD.")]
        [DataRow("株式会社東芝セミコンダクター社(TOSHIBA)")]
        public void TestSuccess(string name)
        {
            var makerName = new MakerName(name);
            Assert.AreEqual(name, makerName.Value);
        }
    }
}

