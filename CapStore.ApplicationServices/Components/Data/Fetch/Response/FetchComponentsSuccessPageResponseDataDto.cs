using System;
using System.Net;

namespace CapStore.ApplicationServices.Components.Data.Fetch.Response
{
	/// <summary>
	/// 電子部品取得成功ページレスポンスデータモデル
	/// </summary>
	public class FetchComponentsSuccessPageResponseDataDto : FetchComponentsPageResponseDataDto
	{
		public FetchComponentsSuccessPageResponseDataDto(FetchComponentListDataDto dto,
														int pageIndex,
														int pageSize) : base(dto, pageIndex, pageSize)
		{
			Success = true;
			StatusCode = dto.Components.Any()
				? HttpStatusCode.OK
				: HttpStatusCode.NoContent;
		}
	}
}