using System;
using CapStore.Domain.Shareds;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Categories
{
	/// <summary>
	/// 電子部品カテゴリーモデル
	/// </summary>
	public class Category
	{
		private readonly CategoryId _id;

		private readonly CategoryName _name;

		private readonly ImageUrl? _imageUrl;


		public Category(CategoryId id, CategoryName name, ImageUrl? imageUrl)
		{
			if(id == null)
			{
				throw new ValidationArgumentNullException("カテゴリーIDは必須です");
			}

			if(name == null)
			{
				throw new ValidationArgumentNullException("カテゴリー名は必須です");
			}

			_id = id;
			_name = name;
			_imageUrl = imageUrl;
		}

		/// <summary>
		/// カテゴリーID
		/// </summary>
		public CategoryId Id => _id;

		/// <summary>
		/// カテゴリー名
		/// </summary>
		public CategoryName Name => _name;

		/// <summary>
		/// カテゴリー画像URL
		/// </summary>
		public ImageUrl? Image => _imageUrl;
	}
}

