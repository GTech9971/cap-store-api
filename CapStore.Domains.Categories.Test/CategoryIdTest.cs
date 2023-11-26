using System;
using CapStore.Domain.Categories;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components.Test
{
	[TestClass]
	public class CategoryIdTest
	{
		private const string CATEGORY = "カテゴリーID";


		[TestMethod]
		[TestCategory(CATEGORY)]
		[ExpectedException(typeof(ValidationArgumentException))]
		[DataRow(-1)]
		public void TestExceptions(int id)
		{
			new CategoryId(id);
		}


        [TestMethod]
        [TestCategory(CATEGORY)]
        [DataRow(0)]
		[DataRow(1)]
		[DataRow(100)]
        public void TestSuccess(int id)
        {
			var categoryId = new CategoryId(id);
			Assert.AreEqual(id, categoryId.Value);
        }
    }
}

