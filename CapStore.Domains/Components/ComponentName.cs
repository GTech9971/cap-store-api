using System;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Components
{
	/// <summary>
	/// 電子部品名
	/// </summary>
	public class ComponentName
	{

		private readonly string _name;

		public ComponentName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ValidationArgumentNullException("電子部品名は必須です");
			}

			_name = name;
		}

		/// <summary>
		/// 電子部品名
		/// </summary>
		public string Value => _name;
	}
}

