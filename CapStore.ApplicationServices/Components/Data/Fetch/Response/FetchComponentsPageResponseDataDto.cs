using System;
using CapStore.ApplicationServices.Categories.Data;
using CapStore.ApplicationServices.Makers.Data;
using CapStore.Domains.Shareds.Responses;

namespace CapStore.ApplicationServices.Components.Data.Fetch.Response
{
	/// <summary>
	/// 電子部品取得ページレスポンスデータモデル
	/// </summary>
	public class FetchComponentsPageResponseDataDto : PageResponse<FetchComponentDataDto<FetchCategoryDataDto, FetchMakerDataDto>>
	{
		public FetchComponentsPageResponseDataDto(FetchComponentListDataDto from,
												  int pageIndex,
												  int pageSize)
			: base(from.Components.ToList(), from.Count, pageIndex, pageSize)
		{
		}
	}
}

