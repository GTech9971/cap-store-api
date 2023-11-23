using System;
namespace CapStore.Domain.Shareds.Exceptions
{
	/// <summary>
	/// モデルの引数がNullの場合の例外
	/// </summary>
	public class ValidationArgumentNullException : ArgumentNullException
	{
		public ValidationArgumentNullException() : base() { }

		public ValidationArgumentNullException(string message) : base(message) { }

		public ValidationArgumentNullException(string message, Exception exception) : base(message, exception) { }
	}
}

