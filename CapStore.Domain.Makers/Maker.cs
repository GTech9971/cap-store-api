using System;
using CapStore.Domain.Shareds;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Makers
{
	/// <summary>
	/// メーカーモデル
	/// </summary>
	public class Maker
	{
		/// <summary>
		/// メーカーなし
		/// </summary>
		/// <returns></returns>
		public static Maker None()
		{
			return new Maker(MakerId.None(), new MakerName("なし"), null);
		}

		private readonly MakerId _id;

		private readonly MakerName _name;

		private readonly ImageUrl? _image;

		public Maker(MakerId id, MakerName name, ImageUrl? image)
		{
			if (id == null)
			{
				throw new ValidationArgumentNullException("メーカーIDは必須です");
			}

			if (name == null)
			{
				throw new ValidationArgumentNullException("メーカー名は必須です");
			}

			_id = id;
			_name = name;
			_image = image;
		}

		/// <summary>
		/// メーカーID
		/// </summary>
		public MakerId Id => _id;

		/// <summary>
		/// メーカー名
		/// </summary>
		public MakerName Name => _name;

		/// <summary>
		/// メーカー画像
		/// </summary>
		public ImageUrl? Image => _image;
	}
}

