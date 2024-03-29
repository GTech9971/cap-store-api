﻿using System;
using CapStore.Domains.Makers;
using CapStore.Domains.Categories;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Components
{
	/// <summary>
	/// 電子部品モデル
	/// </summary>
	public class Component
	{

		private readonly ComponentId _id;
		private readonly ComponentName _name;
		private readonly ComponentModelName _modelName;
		private readonly ComponentDescription _description;
		private readonly Category _category;
		private readonly Maker _maker;
		private readonly ComponentImageList _images;

		public Component(ComponentId id,
						ComponentName name,
						ComponentModelName modelName,
						ComponentDescription description,
						Category category,
						Maker maker,
						ComponentImageList images)
		{

			if (id == null)
			{
				throw new ValidationArgumentNullException("電子部品IDは必須です");
			}

			if (name == null)
			{
				throw new ValidationArgumentNullException("電子部品名は必須です");
			}

			if (modelName == null)
			{
				throw new ValidationArgumentNullException("電子部品モデル名は必須です");
			}

			if (description == null)
			{
				throw new ValidationArgumentNullException("電子部品説明がNullです");
			}

			if (maker == null)
			{
				throw new ValidationArgumentNullException("メーカーは必須です");
			}

			if (category == null)
			{
				throw new ValidationArgumentNullException("カテゴリーは必須です");
			}

			if (images == null)
			{
				throw new ValidationArgumentNullException("画像リストがNullです");
			}

			_id = id;
			_name = name;
			_modelName = modelName;
			_description = description;
			_maker = maker;
			_category = category;
			_images = images;
		}

		/// <summary>
		/// 電子部品ID
		/// </summary>
		public ComponentId Id => _id;
		/// <summary>
		/// 電子部品名
		/// </summary>
		public ComponentName Name => _name;
		/// <summary>
		/// 電子部品モデル名
		/// </summary>
		public ComponentModelName ModelName => _modelName;
		/// <summary>
		/// 電子部品説明
		/// </summary>
		public ComponentDescription Description => _description;
		/// <summary>
		/// 電子部品製造メーカー
		/// </summary>
		public Maker Maker => _maker;
		/// <summary>
		/// 電子部品カテゴリー
		/// </summary>
		public Category Category => _category;
		/// <summary>
		/// 電子部品画像リスト
		/// </summary>
		public ComponentImageList Images => _images;

	}
}

