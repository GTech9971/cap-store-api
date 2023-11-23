using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components
{
	/// <summary>
	/// メーカー名
	/// </summary>
	public class MakerName
	{

		private readonly string _name;

		public MakerName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ValidationArgumentNullException("メーカー名は必須です");
			}

			_name = name;
		}


		/// <summary>
		/// メーカー名
		/// </summary>
		public string Value => _name;
	}
}

