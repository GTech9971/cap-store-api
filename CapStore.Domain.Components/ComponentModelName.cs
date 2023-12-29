using System;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components
{
	/// <summary>
	/// 電子部品モデル名
	/// </summary>
	public class ComponentModelName
	{
		private const string NONE = "なし";
		/// <summary>
		/// モデル名が存在しない
		/// </summary>
		/// <returns></returns>
		public static ComponentModelName None()
		{
			return new ComponentModelName(NONE);
		}

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
		/// モデル名が存在しないかどうか
		/// </summary>
		public bool IsNone
		{
			get { return _modelName == NONE; }
		}

		/// <summary>
		/// 電子部品モデル名
		/// </summary>
		public string Value => _modelName;
	}
}

