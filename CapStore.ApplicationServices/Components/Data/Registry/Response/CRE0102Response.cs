using System;
using System.Net;
using CapStore.ApplicationServices.Categories.Exceptions;
using CapStore.Domains.Categories;
using CapStore.Domains.Shareds;

namespace CapStore.ApplicationServices.Components.Data.Registry.Response
{
	/// <summary>
	/// CRE0102エラーレスポンス
	/// </summary>
	public class CRE0102Response : RegistryComponentErrorResponseDataDto
	{
		public CRE0102Response(CategoryId categoryId) : base(
			new Error(new ErrorCode("CRE0102"),
				new ErrorMessage($"{categoryId}は存在しません"))
			)
		{
			StatusCode = HttpStatusCode.NotFound;
		}

		public CRE0102Response(NotFoundCategoryIdException exception) : base(
			new Error(new ErrorCode("CRE0102"),
				new ErrorMessage(exception.Message))
			)
		{
			StatusCode = HttpStatusCode.NotFound;
		}
	}
}