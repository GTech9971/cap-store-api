﻿using System;
namespace CapStore.Domain.Shareds.Exceptions
{
	/// <summary>
	/// モデルの引数の例外
	/// </summary>
	public class ValidationArgumentException : ArgumentException
	{
		public ValidationArgumentException() : base() { }

		public ValidationArgumentException(string message) : base(message) { }

		public ValidationArgumentException(string message, Exception exception):base(message, exception) { }
	}
}

