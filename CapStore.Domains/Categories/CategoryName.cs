using System;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Categories
{
	/// <summary>
	/// カテゴリー名
	/// </summary>
	public class CategoryName : IEquatable<CategoryName>
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

		public override bool Equals(object obj)
		{
			return Equals(obj as CategoryName);
		}

		public bool Equals(CategoryName other)
		{
			return other != null && _name == other.Value;
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode();
		}
	}
}

