using System;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Categories
{
	/// <summary>
	/// カテゴリーID
	/// </summary>
	public class CategoryId
	{

		private const int UNDETECT_ID = 999999;

		/// <summary>
		/// 未定義のカテゴリーID(999999)
		/// </summary>
		/// <returns></returns>
		public static CategoryId UnDetectId()
		{
			return new CategoryId(UNDETECT_ID);
		}

		private readonly int _id;

		public CategoryId(int id)
		{
			if (id < 0)
			{
				throw new ValidationArgumentException("カテゴリーIDがマイナスです");
			}

			_id = id;
		}

		/// <summary>
		/// カテゴリーID
		/// </summary>
		public int Value => _id;

		/// <summary>
		/// 未確定のIDかどうか
		/// </summary>
		/// <returns></returns>
		public bool IsUnDetect
		{
			get
			{
				return _id == UNDETECT_ID;
			}
		}

		public override string ToString()
		{
			return $"カテゴリーID:{_id}";
		}
	}
}

