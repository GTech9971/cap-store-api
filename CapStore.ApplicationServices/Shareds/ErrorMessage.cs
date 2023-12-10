using System;
namespace CapStore.ApplicationServices.Shareds
{
	/// <summary>
	/// エラーメッセージ
	/// </summary>
	public class ErrorMessage
	{

		private readonly string _message;

		public ErrorMessage(string message)
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				throw new ArgumentNullException("エラーメッセージは必須です");
			}

			_message = message;
		}

		/// <summary>
		/// エラーメッセージ
		/// </summary>
		public string Value => _message;
	}
}

