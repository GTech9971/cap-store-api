using System;
using System.Text.Json.Serialization;
using CapStore.Domain.Categories;

namespace CapStore.ApplicationServices.Categories.Data
{
	/// <summary>
	/// カテゴリー取得データモデル
	/// </summary>
	public class FetchCategoryDataDto
	{
		public FetchCategoryDataDto(Category from)
		{
			Id = from.Id.Value;
			Name = from.Name.Value;
			Image = from.Image?.Value;
		}

		[JsonPropertyName("id")]
		public int Id { get; }

		[JsonPropertyName("name")]
		public string Name { get; }

		[JsonPropertyName("image")]	
		public string? Image { get; }
	}
}

