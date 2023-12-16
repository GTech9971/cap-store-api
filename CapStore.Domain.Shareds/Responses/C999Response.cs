using System;
using System.Net;

namespace CapStore.Domain.Shareds.Responses
{
	/// <summary>
	/// 予期せぬ例外発生時のレスポンス
	/// </summary>
	public class C999Response : BaseResponse<object>
	{

		private readonly ErrorCode CODE = new ErrorCode("C999");

		public C999Response(Exception ex) : base()
		{
			Success = false;
			StatusCode = HttpStatusCode.InternalServerError;
			Errors = new List<Error>()
			{
				new Error(CODE,
				new ErrorMessage($"予期せぬ例外が発生しました.{ex.Message}"),
				ex.StackTrace)
			};
		}
	}
}