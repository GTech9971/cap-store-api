using System;
using System.Net;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Shareds.Responses
{

	/// <summary>
	/// 入力バリデーションエラーレスポンス
	/// </summary>
	public class C400Response : BaseResponse<Object>
	{

		private readonly ErrorCode CODE = new ErrorCode("C400");

		public C400Response(ValidationException exception) : base()
		{
			Success = false;
			StatusCode = HttpStatusCode.BadRequest;
			Errors = new List<Error>()
			{
				new Error(CODE,
				new ErrorMessage(exception.Message))
			};
		}

		public C400Response(IEnumerable<ValidationException> exceptions) : base()
		{
			Success = false;
			StatusCode = HttpStatusCode.BadRequest;

			Errors = new List<Error>(
					exceptions.Select(x => new Error(
							CODE,
							new ErrorMessage(x.Message)
						))
				);
		}
	}
}

