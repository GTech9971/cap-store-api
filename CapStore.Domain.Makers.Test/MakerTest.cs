using System;
using CapStore.Domain.Makers;
using CapStore.Domain.Shareds;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components.Test
{
	[TestClass]
	public class MakerTest
	{
        private const string CATEGORY = "メーカー";

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentNullException))]        
        public void TestNullIdExceptions()
        {
            new Maker(null!, new MakerName("TEST"), new ImageUrl("https://akizukidenshi.com/img/goods/L/I-18167.jpg"));
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentNullException))]
        public void TestNullNameExceptions()
        {
            new Maker(new MakerId(0), null!, new ImageUrl("https://akizukidenshi.com/img/goods/L/I-18167.jpg"));
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow(0, "Microchip Technology Inc.(マイクロチップ)/Atmel Corporation(アトメル)", "https://akizukidenshi.com/img/goods/3/I-18167.jpg")]
        [DataRow(1,"Sipeed", null)]
        [DataRow(2,"株式会社村田製作所(muRata)", null)]
        [DataRow(3,"PARA LIGHT ELECTRONICS CO., LTD.", null)]
        [DataRow(100,"株式会社東芝セミコンダクター社(TOSHIBA)", null)]
        public void TestSuccess(int id, string name, string url)
        {
            var makerId = new MakerId(id);
            var makerName = new MakerName(name);
            ImageUrl? image = null;
            if(string.IsNullOrWhiteSpace(url) == false)
            {
                image = new ImageUrl(url);
            }

            var maker = new Maker(makerId, makerName, image);
            Assert.AreEqual(id, maker.Id.Value);
            Assert.AreEqual(name, maker.Name.Value);
            Assert.AreEqual(url, maker.Image?.Value); 
        }
    }
}

