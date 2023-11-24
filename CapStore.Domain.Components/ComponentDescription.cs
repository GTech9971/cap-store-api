using System;
using CapStore.Domain.Shareds.Exceptions;
namespace CapStore.Domain.Components
{
	/// <summary>
	/// 電子部品説明
	/// </summary>
	public class ComponentDescription
	{

		private const string EMPTY = "説明文なし";

        /// <summary>
        /// 空の説明文
        /// </summary>
        /// <returns></returns>
        public static ComponentDescription Empty()
		{
			return new ComponentDescription(EMPTY);
		}

		private readonly string _description;

		public ComponentDescription(string description)
		{
			if (string.IsNullOrWhiteSpace(description))
			{
				throw new ValidationArgumentNullException("説明文は必須です");
			}

			_description = description;
		}

		/// <summary>
		/// 電子部品説明
		/// </summary>
		public string Value => _description;

		/// <summary>
		/// 説明文がないかどうか
		/// </summary>
		/// <returns></returns>
		public bool IsEmpty()
		{
			return _description == EMPTY;
		}
	}
}

