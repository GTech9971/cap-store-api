using System;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Makers
{
	/// <summary>
	/// メーカーID
	/// </summary>
	public class MakerId
	{
		/// <summary>
		/// メーカーが存在しない
		/// </summary>
		private const int NONE_ID = 999990;

		/// <summary>
		/// メーカー未確定
		/// </summary>
		private const int UNDETECT_ID = 999999;

		/// <summary>
		/// 未確定メーカーID
		/// </summary>
		/// <returns></returns>
		public static MakerId UnDetect()
		{
			return new MakerId(UNDETECT_ID);
		}

		/// <summary>
		/// メーカーなし
		/// </summary>
		/// <returns></returns>
		public static MakerId None()
		{
			return new MakerId(NONE_ID);
		}

		private readonly int _id;

		public MakerId(int id)
		{
			if (id < 0)
			{
				throw new ValidationArgumentException("メーカーIDがマイナスです");
			}

			_id = id;
		}

		/// <summary>
		/// メーカーID
		/// </summary>
		public int Value => _id;

		/// <summary>
		/// 未確定かどうか
		/// </summary>
		/// <returns></returns>
		public bool IsUnDetect
		{
			get
			{
				return _id == UNDETECT_ID;
			}
		}

		/// <summary>
		/// メーカーが存在しないかどうか
		/// </summary>
		public bool IsNone
		{
			get
			{
				return _id == NONE_ID;
			}
		}

		public override string ToString()
		{
			return $"メーカーID:{_id}";
		}
	}
}

