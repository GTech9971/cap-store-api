using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CapStore.ApplicationServices.Components.Data.Registry
{
	/// <summary>
	/// 電子部品登録リクエストデータ
	/// </summary>
	public class RegistryComponentRequestDataDto
	{

		public RegistryComponentRequestDataDto()
		{
		}

		[Required]
		[JsonPropertyName("name")]
		public string Name { get; set; } = null!;

		[Required]
		[JsonPropertyName("modelName")]
		public string ModelName { get; set; } = null!;

		[Required]
		[JsonPropertyName("description")]
		public string Description { get; set; } = null!;

		[Required]
		[JsonPropertyName("categoryId")]
		public int CategoryId { get; set; }

		[Required]
		[JsonPropertyName("makerId")]
		public int MakerId { get; set; }

		[JsonPropertyName("images")]
		public List<string>? Images { get; set; }
	}
}

