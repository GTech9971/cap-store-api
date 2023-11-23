using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components
{
	/// <summary>
	/// 電子部品ID
	/// </summary>
	public class ComponentId
	{
		public const int MIN_VALUE = 100;


		private readonly int _componentId;

		public ComponentId(int componentId)
		{
			if(componentId < 0)
			{
				throw new ValidationArgumentException($"電子部品IDの値がマイナスです。");
			}

			if(componentId < MIN_VALUE)
			{
				throw new ValidationArgumentException($"電子部品IDの値が最小値:{MIN_VALUE}より小さいです。");
			}


			_componentId = componentId;
		}

		/// <summary>
		/// 電子部品ID
		/// </summary>
		public int Value => _componentId;
		
	}
}

