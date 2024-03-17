using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Categories;
using CapStore.Domains.Makers;
using System.Text.Json.Serialization;

namespace CapStore.ApplicationServices.Akizukies.Catalogs.Data.Fetch
{
    /// <summary>
    /// 秋月電子から電子部品の情報取得したデータモデル
    /// </summary>
    public class FetchAkizukiPageDataDto<CT, MT> where CT : FetchCategoryDataDto where MT : FetchMakerDataDto
    {
        public FetchAkizukiPageDataDto(AkizukiPage from)
        {
            ComponentId = from.Component.Id.Value;
            Name = from.Component.Name.Value;
            ModelName = from.Component.ModelName.Value;
            Description = from.Component.Description.Value;
            Category = (CT)new FetchCategoryDataDto(from.Component.Category);
            Maker = (MT)new FetchMakerDataDto(from.Component.Maker);
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
        public CT Category { get; }

        [JsonPropertyName("maker")]
        public MT Maker { get; }

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

        [JsonPropertyName("categoryId")]
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

        [JsonPropertyName("makerId")]
        public int Id { get; }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("image")]
        public string? Image { get; }
    }
}
