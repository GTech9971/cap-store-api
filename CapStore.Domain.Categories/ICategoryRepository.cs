using System;
using CapStore.Domain.Shareds;

namespace CapStore.Domain.Categories
{
	/// <summary>
	/// カテゴリーの永続化に関連する操作を行うレポジトリ
	/// </summary>
	public interface ICategoryRepository
	{

		Task<Category?> Fetch(CategoryId categoryId);
		Task<Category?> Fetch(CategoryName categoryName);

		Task<Category> Save(Category category);
	}
}

