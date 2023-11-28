﻿using System;
using CapStore.Domain.Categories;
using CapStore.Infrastructure.Ef.Categories;

namespace CapStore.Infrastructure.Ef.Test
{
	[TestClass]
	public class EfCategoryRepositoryTest : BaseEfRepositoryTest
	{
		
		private const string CATEGORY = "カテゴリーレポジトリEF";		


		[TestMethod]
		[TestCategory(CATEGORY)]
		[DataRow("マイコン")]
		[DataRow("抵抗")]
		public async Task SaveSuccessTest(string name)
		{
			CategoryName categoryName = new CategoryName(name);
			Category category = new Category(CategoryId.UnDetectId(), categoryName, null);

			using (var context = CreateContext())
			{
				ICategoryRepository repository = new EfCategoryRepository(context);
				//登録テスト
				Category registredCategory = await repository.Save(category);

				Category? found = await repository.Fetch(categoryName);
				Assert.IsNotNull(found);
				Assert.IsFalse(found.Id.IsUnDetect);

				Assert.AreEqual(name, registredCategory.Name.Value);
				Assert.IsFalse(registredCategory.Id.IsUnDetect);

                //更新テスト
                CategoryName changeCategoryName = new CategoryName("change");
                Category updateNameCategory = new Category(registredCategory.Id, changeCategoryName, null);
                Category updatedCategory = await repository.Save(updateNameCategory);

                Assert.AreEqual(found.Id.Value, updatedCategory.Id.Value);
                Assert.AreEqual(changeCategoryName.Value, updatedCategory.Name.Value);
            }
		}


	}
}

