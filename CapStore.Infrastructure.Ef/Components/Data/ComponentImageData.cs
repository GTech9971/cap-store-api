using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CapStore.Domain.Components;
using CapStore.Domain.Shareds;

namespace CapStore.Infrastructure.Ef.Components.Data
{
	/// <summary>
	/// efで使用する電子部品の画像URLデータモデル
	/// </summary>
	[Table("component_images")]
	public class ComponentImageData
	{
		/// <summary>
		/// 主キー
		/// </summary>
		[Key]
		[Column("id")]
		public int Id { get; set; }

		/// <summary>
		/// 電子部品ID
		/// </summary>
		[Column("component_id_fk")]
		public int ComponentId { get; set; }

		[ForeignKey(nameof(ComponentId))]
		public ComponentData Component { get; set; } = null!;

		/// <summary>
		/// 電子部品画像URL
		/// </summary>
		[Required]
		[DataType(DataType.ImageUrl)]
		[Column("image_url")]
		public string ImageUrl { get; set; } = null!;

		public ComponentImageData() { }

		public ComponentImageData(ComponentImage from)
		{
			if (from.ComponentImageId.IsUnDetect == false)
			{
				Id = from.ComponentImageId.Value;
			}

			if (from.ComponentId.IsUnDetect == false)
			{
				ComponentId = from.ComponentId.Value;
			}

			ImageUrl = from.Image.Value;
		}

		public ComponentImage ToModel()
		{
			return new ComponentImage(
							new ComponentImageId(Id),
							new ComponentId(ComponentId),
							new ImageUrl(ImageUrl));
		}
	}
}

