using System;
using CapStore.ApplicationServices.Categories.Data;
using CapStore.ApplicationServices.Makers.Data;
namespace CapStore.ApplicationServices.Components.Data.Fetch
{
	/// <summary>
	/// 電子部品取得結果リストデータモデル
	/// </summary>
	public class FetchComponentListDataDto
	{
		public FetchComponentListDataDto(IEnumerable<FetchComponentDataDto<FetchCategoryDataDto, FetchMakerDataDto>> dtos,
			int count)
		{
			Components = dtos;
			Count = count;
		}

		public IEnumerable<FetchComponentDataDto<FetchCategoryDataDto, FetchMakerDataDto>> Components { get; }

		/// <summary>
		/// DB常にある全件数
		/// </summary>
		public int Count { get; }
	}
}

