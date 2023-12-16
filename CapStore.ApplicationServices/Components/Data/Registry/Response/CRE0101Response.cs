using System;
using System.Net;
using CapStore.Domain.Shareds;

namespace CapStore.ApplicationServices.Components.Data.Registry.Response
{
	/// <summary>
	/// CRE0101エラーレスポンス
	/// </summary>
	public class CRE0101Response : RegistryComponentErrorResponseDataDto
	{
		public CRE0101Response() :
			base(new Error(new ErrorCode("CRE0101"),
							new ErrorMessage("電子部品がすでに登録されています")))
		{
			StatusCode = HttpStatusCode.BadRequest;
		}
	}
}