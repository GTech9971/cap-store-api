using System;
namespace CapStore.Domain.Shareds
{
	/// <summary>
	/// エラーコード
	/// </summary>
	public class ErrorCode
	{
		private const int MIN_LENGTH = 4;
		private const int MAX_LENGTH = 7;
		private readonly string _code;

		public ErrorCode(string code)
		{
			if (string.IsNullOrWhiteSpace(code))
			{
				throw new ArgumentNullException("エラーコードは必須です");
			}

			if (code.Length < MIN_LENGTH)
			{
				throw new ArgumentException($"エラーコードが短すぎます。最小{MIN_LENGTH}桁です。");
			}

			if (code.Length > MAX_LENGTH)
			{
				throw new ArgumentException($"エラーコードが長すぎます。最大{MAX_LENGTH}桁です。");
			}

			_code = code;
		}

		/// <summary>
		/// エラーコード
		/// </summary>
		public string Value => _code;
	}
}

