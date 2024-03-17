using System;
namespace CapStore.Domains.Categories.Services
{
	/// <summary>
	/// カテゴリーサービス
	/// </summary>
	public class CategoryService
	{

		private readonly ICategoryRepository _repository;

		public CategoryService(ICategoryRepository repository)
		{
			_repository = repository;
		}


		/// <summary>
		/// カテゴリー名が重複しないカテゴリーが存在するか調べる
		/// </summary>
		/// <param name="categoryName">カテゴリー名</param>
		/// <returns></returns>
		public async Task<bool> Exists(CategoryName categoryName)
		{
			Category? category = await _repository.Fetch(categoryName);
			return category != null;
		}
	}
}

