using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Makers
{
	/// <summary>
	/// メーカーID
	/// </summary>
	public class MakerId
	{

		private const int UNDETECT_ID = 999999;

		/// <summary>
		/// 未確定メーカーID
		/// </summary>
		/// <returns></returns>
		public static MakerId UnDetect()
		{
			return new MakerId(UNDETECT_ID);
		}

		private readonly int _id;

		public MakerId(int id)
		{
			if(id < 0)
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
	}
}

