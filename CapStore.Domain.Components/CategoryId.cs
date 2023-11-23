using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components
{
	/// <summary>
	/// カテゴリーID
	/// </summary>
	public class CategoryId
	{

		private readonly int _id;

		public CategoryId(int id)
		{
			if(id < 0)
			{
				throw new ValidationArgumentException("カテゴリーIDがマイナスです");
			}

			_id = id;
		}

		/// <summary>
		/// カテゴリーID
		/// </summary>
		public int Value => _id;
	}
}

