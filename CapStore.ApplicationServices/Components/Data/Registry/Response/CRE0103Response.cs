using System;
using System.Net;
using CapStore.ApplicationServices.Makers.Exceptions;
using CapStore.Domains.Makers;
using CapStore.Domains.Shareds;

namespace CapStore.ApplicationServices.Components.Data.Registry.Response
{
	/// <summary>
	/// CRE0103レスポンス
	/// </summary>
	public class CRE0103Response : RegistryComponentErrorResponseDataDto
	{
		public CRE0103Response(MakerId makerId) :
			base(new Error(new ErrorCode("CRE0103"),
				new ErrorMessage($"{makerId}は存在しません")))
		{
			StatusCode = HttpStatusCode.NotFound;
		}
		public CRE0103Response(NotFoundMakerIdException exception) :
			base(new Error(new ErrorCode("CRE0103"),
				new ErrorMessage(exception.Message)))
		{
			StatusCode = HttpStatusCode.NotFound;
		}
	}
}