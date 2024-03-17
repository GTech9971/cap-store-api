using System;
using CapStore.Domains.Shareds;

namespace CapStore.Domains.Categories
{
	/// <summary>
	/// カテゴリーの永続化に関連する操作を行うレポジトリ
	/// </summary>
	public interface ICategoryRepository
	{

		IAsyncEnumerable<Category> FetchAll();

		Task<Category?> Fetch(CategoryId categoryId);
		Task<Category?> Fetch(CategoryName categoryName);

		Task<Category> Save(Category category);
	}
}

