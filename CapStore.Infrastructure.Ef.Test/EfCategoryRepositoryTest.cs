using System;
using CapStore.Domain.Categories;
using CapStore.Infrastructure.Ef.Categories;
using Xunit;

namespace CapStore.Infrastructure.Ef.Test
{
	public class EfCategoryRepositoryTest : IClassFixture<BaseEfRepositoryTest>
	{

		private readonly CapStoreDbContext _context;

		public EfCategoryRepositoryTest(BaseEfRepositoryTest fixture)
		{
			_context = fixture._context;
		}

		private const string CATEGORY = "カテゴリーレポジトリEF";


		[Theory(DisplayName = "カテゴリー保存成功テスト")]
		[Trait("Category", CATEGORY)]
		[InlineData("マイコン")]
		[InlineData("抵抗")]
		public async Task SaveSuccessTest(string name)
		{
			CategoryName categoryName = new CategoryName(name);
			Category category = new Category(CategoryId.UnDetectId(), categoryName, null);


			ICategoryRepository repository = new EfCategoryRepository(_context);
			//登録テスト
			Category registredCategory = await repository.Save(category);

			Category? found = await repository.Fetch(categoryName);
			Assert.NotNull(found);
			Assert.False(found.Id.IsUnDetect);

			Assert.Equal(name, registredCategory.Name.Value);
			Assert.False(registredCategory.Id.IsUnDetect);

			//更新テスト
			CategoryName changeCategoryName = new CategoryName("change");
			Category updateNameCategory = new Category(registredCategory.Id, changeCategoryName, null);
			Category updatedCategory = await repository.Save(updateNameCategory);

			Assert.Equal(found.Id.Value, updatedCategory.Id.Value);
			Assert.Equal(changeCategoryName.Value, updatedCategory.Name.Value);
		}
	}
}

