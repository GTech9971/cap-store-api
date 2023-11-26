using System;
using CapStore.Domain.Categories;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components.Test
{
	[TestClass]
	public class CategoryNameTest
	{

		private const string CATEGORY = "カテゴリー名";


		[TestMethod]
		[TestCategory(CATEGORY)]
		[ExpectedException(typeof(ValidationArgumentNullException))]
		public void TestNullExceptions()
		{
			new CategoryName(null!);
		}

        [TestMethod]
        [TestCategory(CATEGORY)]
        [ExpectedException(typeof(ValidationArgumentNullException))]
        public void TestWhiteSpaceExceptions()
        {
            new CategoryName("");
        }

        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow("PIC")]
        [DataRow("半導体")]
        [DataRow("抵抗")]
        [DataRow("LED・抵抗")]
        public void TestSuccess(string name)
        {
            var categoryName = new CategoryName(name);
            Assert.AreEqual(name, categoryName.Value);            
        }
    }
}

