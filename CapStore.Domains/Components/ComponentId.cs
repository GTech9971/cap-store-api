using System;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Components
{
	/// <summary>
	/// 電子部品ID
	/// </summary>
	public class ComponentId
	{

		private const int UNDETECT_ID = 999999;

		/// <summary>
		/// 未確定の電子部品ID
		/// </summary>
		/// <returns></returns>
		public static ComponentId UnDetectId()
		{
			return new ComponentId(UNDETECT_ID);
		}

		private readonly int _componentId;

		public ComponentId(int componentId)
		{
			if (componentId < 0)
			{
				throw new ValidationArgumentException($"電子部品IDの値がマイナスです。");
			}

			_componentId = componentId;
		}

		/// <summary>
		/// 電子部品ID
		/// </summary>
		public int Value => _componentId;


		/// <summary>
		/// 未確定かどうか
		/// </summary>
		/// <returns></returns>
		public bool IsUnDetect
		{
			get { return _componentId == UNDETECT_ID; }
		}
	}
}

