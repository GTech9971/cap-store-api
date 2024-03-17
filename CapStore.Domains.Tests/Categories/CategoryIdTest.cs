using System;
using CapStore.Domains.Categories;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Components.Test
{
	public class CategoryIdTest
	{
		private const string CATEGORY = "カテゴリーID";


		[Theory]
		[Trait("Category", CATEGORY)]
		[InlineData(-1)]
		public void TestExceptions(int id)
		{
			Assert.Throws<ValidationArgumentException>(() =>
			{
				new CategoryId(id);
			});
		}


		[Theory]
		[Trait("Category", CATEGORY)]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(100)]
		public void TestSuccess(int id)
		{
			var categoryId = new CategoryId(id);
			Assert.Equal(id, categoryId.Value);
		}
	}
}

