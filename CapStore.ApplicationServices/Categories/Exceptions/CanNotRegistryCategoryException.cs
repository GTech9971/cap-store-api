using System;
using CapStore.Domains.Categories;

namespace CapStore.ApplicationServices.Categories.Exceptions
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

