using System;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Makers
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

		public override string ToString()
		{
			return _name;
		}
	}
}

