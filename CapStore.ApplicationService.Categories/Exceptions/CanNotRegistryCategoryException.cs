using System;
using CapStore.Domain.Categories;

namespace CapStore.ApplicationService.Categories.Exceptions
{
	/// <summary>
	/// カテゴリー登録時の例外
	/// </summary>
	public class CanNotRegistryCategoryException : Exception
	{
		public CanNotRegistryCategoryException(Category category, string message)
			: base($"{message}:{category.ToString()}")
		{
		}
	}
}

