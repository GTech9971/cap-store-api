using System;
namespace CapStore.Domain.Components
{
	/// <summary>
	/// 電子部品モデル
	/// </summary>
	public class Component
	{

		private readonly ComponentId _id;
		private readonly ComponentName _name;
		private readonly ComponentModelName _modelName;
		private readonly ComponentDescription _description;
		private readonly Category _category;
		private readonly Maker _maker;
		private readonly ComponentImages _images;

		public Component()
		{
		}
	}
}

