using System;
namespace CapStore.ApplicationServices.Components.Data.Fetch
{
	/// <summary>
	/// 電子部品取得結果リストデータモデル
	/// </summary>
	public class FetchComponentListDataDto
	{
		public FetchComponentListDataDto(IEnumerable<FetchComponentDataDto> dtos,
			int count)
		{
			Components = dtos;
			Count = count;
		}

		public IEnumerable<FetchComponentDataDto> Components { get; }

		/// <summary>
		/// DB常にある全件数
		/// </summary>
		public int Count { get; }
	}
}

