using System;
namespace CapStore.Domain.Shareds.Exceptions
{

	/// <summary>
	/// モデルのバリデーション例外
	/// </summary>
	public class ValidationException : Exception
	{

		public ValidationException() : base() { }

		public ValidationException(string message) : base(message) { }

		public ValidationException(string message, Exception exception) : base(message, exception) { }
	}
}

