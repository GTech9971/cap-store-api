using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Shareds.Test
{
	[TestClass]
	public class ImageUrlTest
	{

		private const string CATEGORY = "画像URL";

		[TestMethod]
		[TestCategory(CATEGORY)]
		[ExpectedException(typeof(ValidationArgumentNullException))]
		public void TestNullExceptions()
		{
			new ImageUrl("");
		}

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentException))]
		[DataRow("aaa")]
		[DataRow("http:/aas")]
        public void TestFormatExceptions(string url)
        {
            new ImageUrl(url);
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("https://akizukidenshi.com/img/goods/S/M-18150.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/S/I-18269.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/L/M-18150.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/3/M-18150.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/L/I-04430.jpg")]
        [DataRow("https://akizukidenshi.com/img/goods/2/I-04430.jpg")]
        public void TestSuccess(string url)
        {
            var imageUrl = new ImageUrl(url);
            Assert.AreEqual(url, imageUrl.Value);
        }
    }
}

