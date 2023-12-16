using Akizuki.Domain.Catalogs;
using CapStore.Domain.Categories;
using CapStore.Domain.Makers;
using System.Text.Json.Serialization;

namespace Akizuki.ApplicationService.Catalogs.Data.Fetch
{
    /// <summary>
    /// 秋月電子から電子部品の情報取得したデータモデル
    /// </summary>
    public class FetchAkizukiPageDataDto
    {
        public FetchAkizukiPageDataDto(AkizukiPage from)
        {
            ComponentId = from.Component.Id.Value;
            Name = from.Component.Name.Value;
            ModelName = from.Component.ModelName.Value;
            Description = from.Component.Description.Value;
            Category = new FetchCategoryDataDto(from.Component.Category);
            Maker = new FetchMakerDataDto(from.Component.Maker);
            Images = from.Component.Images.AsList().Select(x => x.Image.Value);
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
        public FetchCategoryDataDto Category { get; }

        [JsonPropertyName("maker")]
        public FetchMakerDataDto Maker { get; }

        [JsonPropertyName("images")]
        public IEnumerable<string> Images { get; }
    }

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
