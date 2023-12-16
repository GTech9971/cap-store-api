using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Categories
{
	/// <summary>
	/// カテゴリー名
	/// </summary>
	public class CategoryName
	{
		private readonly string _name;

		public CategoryName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ValidationArgumentNullException("カテゴリー名は必須です");
			}

			_name = name;
		}

		/// <summary>
		/// カテゴリー名
		/// </summary>
		public string Value => _name;

		public override string ToString()
		{
			return _name;
		}
	}
}

