﻿using System;
using System.Net;
using CapStore.ApplicationServices.Shareds;
using CapStore.Domain.Categories;

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
	}
}