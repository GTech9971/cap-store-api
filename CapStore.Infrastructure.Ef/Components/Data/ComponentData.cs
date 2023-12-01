using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CapStore.Domain.Components;
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
			//TODO
		}
	}
}

