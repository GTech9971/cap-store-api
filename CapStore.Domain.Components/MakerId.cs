using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components
{
	/// <summary>
	/// メーカーID
	/// </summary>
	public class MakerId
	{

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
	}
}

