using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CapStore.Domain.Categories;
using CapStore.Domain.Components;
using CapStore.Domain.Makers;
using CapStore.Infrastructure.Ef.Categories.Data;
using CapStore.Infrastructure.Ef.Makers.Data;

namespace CapStore.Infrastructure.Ef.Components.Data
{
	/// <summary>
	/// efで使用する電子部品のデータモデル
	/// </summary>
	[Table("components")]
	public class ComponentData
	{
		/// <summary>
		/// 主キー
		/// </summary>
		[Key]
		[Column("component_id")]
		public int ComponentId { get; set; }

		/// <summary>
		/// 電子部品名
		/// </summary>
		[Required]
		[Column("name")]
		public string Name { get; set; } = null!;

		/// <summary>
		/// モデル名
		/// </summary>
		[Required]
		[Column("model_name")]
		public string ModelName { get; set; } = null!;

		/// <summary>
		/// 説明
		/// </summary>
		[Required]
		[Column("description")]
		public string Description { get; set; } = null!;

		/// <summary>
		/// カテゴリーID
		/// </summary>
		[Required]
		[Column("category_id")]
		public int CategoryId { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public CategoryData CategoryData { get; set; } = null!;

		/// <summary>
		/// メーカーID
		/// </summary>
		[Required]
		[Column("maker_id")]
		public int MakerId { get; set; }

		[ForeignKey(nameof(MakerId))]
		public MakerData MakerData { get; set; } = null!;

		/// <summary>
		/// 電子部品画像リスト
		/// </summary>
		public ICollection<ComponentImageData> ComponentImageDatas { get; set; } = null!;

		public ComponentData() { }

		public ComponentData(Component from)
		{
			if (from.Id.IsUnDetect == false)
			{
				ComponentId = from.Id.Value;
			}

			Name = from.Name.Value;
			ModelName = from.ModelName.Value;
			Description = from.Description.Value;
			//category
			CategoryId = from.Category.Id.Value;
			//以下の変換を行うとEFエラーが発生するので、IDのみを変換させる
			//CategoryData = new CategoryData(from.Category);

			//maker
			MakerId = from.Maker.Id.Value;
			//以下の変換を行うとEFエラーが発生するので、IDのみを変換させる
			//MakerData = new MakerData(from.Maker);

			//images
			ComponentImageDatas = from.Images
				.AsList()
				.Select(x => new ComponentImageData(x))
				.ToList();
		}

		public Component ToModel()
		{
			ComponentId componentId = new ComponentId(ComponentId);
			ComponentName componentName = new ComponentName(Name);
			ComponentModelName componentModelName = new ComponentModelName(ModelName);
			ComponentDescription componentDescription = new ComponentDescription(Description);
			Category category = CategoryData.ToModel();
			Maker maker = MakerData.ToModel();
			ComponentImageList componentImageList = new ComponentImageList(
						ComponentImageDatas.Select(x => x.ToModel())
				);
			return new Component(
						componentId,
						componentName,
						componentModelName,
						componentDescription,
						category,
						maker,
						componentImageList
				); ;
		}
	}
}