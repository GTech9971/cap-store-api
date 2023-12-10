using System;
using CapStore.ApplicationServices.Shareds;

namespace CapStore.ApplicationServices.Components.Data.Fetch.Response
{
	/// <summary>
	/// 電子部品取得ページレスポンスデータモデル
	/// </summary>
	public class FetchComponentsPageResponseDataDto:PageResponse<FetchComponentDataDto>
	{
		public FetchComponentsPageResponseDataDto(FetchComponentListDataDto from,
												  int pageIndex,
												  int pageSize)
			: base(from.Components.ToList(), from.Count, pageIndex, pageSize)
		{
		}
	}
}

