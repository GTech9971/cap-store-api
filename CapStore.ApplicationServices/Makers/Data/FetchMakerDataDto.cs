using System;
using System.Text.Json.Serialization;
using CapStore.Domain.Makers;

namespace CapStore.ApplicationServices.Makers.Data
{
	/// <summary>
	/// メーカー取得データモデル
	/// </summary>
	public class FetchMakerDataDto
	{
		public FetchMakerDataDto(Maker from)
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

