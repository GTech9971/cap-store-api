using System;
namespace CapStore.Domains.Shareds.Exceptions
{
	/// <summary>
	/// モデルの引数がNullの場合の例外
	/// </summary>
	public class ValidationArgumentNullException : ValidationException
	{
		public ValidationArgumentNullException() : base() { }

		public ValidationArgumentNullException(string message) : base(message) { }

		public ValidationArgumentNullException(string message, Exception exception) : base(message, exception) { }
	}
}

