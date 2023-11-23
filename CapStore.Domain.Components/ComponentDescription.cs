using System;
namespace CapStore.Domain.Components
{
	/// <summary>
	/// 電子部品説明
	/// </summary>
	public class ComponentDescription
	{

		private readonly string _description;

		public ComponentDescription(string description)
		{
			_description = description;
		}

		/// <summary>
		/// 電子部品説明
		/// </summary>
		public string Value => _description;
	}
}

