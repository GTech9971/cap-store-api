using System;
using System.Text.Json.Serialization;
using CapStore.ApplicationServices.Categories.Data;
using CapStore.ApplicationServices.Makers.Data;
using CapStore.Domain.Components;

namespace CapStore.ApplicationServices.Components.Data.Fetch
{
	/// <summary>
	/// 電子部品取得データモデル
	/// </summary>
	public class FetchComponentDataDto<CT, MT> where CT : FetchCategoryDataDto where MT : FetchMakerDataDto
	{
		public FetchComponentDataDto(Component from)
		{
			ComponentId = from.Id.Value;
			Name = from.Name.Value;
			ModelName = from.ModelName.Value;
			Description = from.Description.Value;
			Category = (CT)new FetchCategoryDataDto(from.Category);
			Maker = (MT)new FetchMakerDataDto(from.Maker);
			Images = from.Images.AsList().Select(x => x.Image.Value);
		}

		[JsonPropertyName("componentId")]
		public int ComponentId { get; }

		[JsonPropertyName("name")]
		public string Name { get; }

		[JsonPropertyName("modelName")]
		public string ModelName { get; }

		[JsonPropertyName("description")]
		public string Description { get; }

		[JsonPropertyName("category")]
		public CT Category { get; }

		[JsonPropertyName("maker")]
		public MT Maker { get; }

		[JsonPropertyName("images")]
		public IEnumerable<string> Images { get; }
	}
}

