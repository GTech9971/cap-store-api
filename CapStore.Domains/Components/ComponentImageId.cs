using System;
using System.ComponentModel.DataAnnotations;

namespace CapStore.Domains.Components
{
	/// <summary>
	/// 電子部品画像UrlID
	/// </summary>
	public class ComponentImageId
	{

		private const int UNDETECT_ID = 999999;

		/// <summary>
		/// 未確定の電子部品画像UrlID
		/// </summary>
		/// <returns></returns>
		public static ComponentImageId UnDetectId()
		{
			return new ComponentImageId(UNDETECT_ID);
		}

		private readonly int _id;

		public ComponentImageId(int id)
		{
			if (id < 0)
			{
				throw new ValidationException("IDがマイナスです");
			}
			_id = id;
		}

		/// <summary>
		/// 電子部品画像UurlID
		/// </summary>
		public int Value => _id;

		/// <summary>
		/// 未確定IDかどうか
		/// </summary>
		/// <returns></returns>
		public bool IsUnDetect
		{
			get { return _id == UNDETECT_ID; }
		}
	}
}

