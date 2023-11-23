using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components
{
	/// <summary>
	/// 電子部品モデル名
	/// </summary>
	public class ComponentModelName
	{

		private readonly string _modelName;

		public ComponentModelName(string modelName)
		{
			if (string.IsNullOrWhiteSpace(modelName))
			{
				throw new ValidationArgumentNullException("電子部品モデル名は必須です");
			}

			_modelName = modelName;
		}

		/// <summary>
		/// 電子部品モデル名
		/// </summary>
		public string Value => _modelName;
	}
}

