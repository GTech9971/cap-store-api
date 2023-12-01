using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CapStore.Domain.Components;

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
		public int Id { get; set; }

		/// <summary>
		/// 電子部品ID
		/// </summary>
		[Column("component_id")]
		public int ComponentId { get; set; }

		/// <summary>
		/// 電子部品画像URL
		/// </summary>
		[Required]
		[DataType(DataType.ImageUrl)]
		[Column("image_url")]
		public string ImageUrl { get; set; } = null!;

		public ComponentImageData() { }

		public ComponentImageData(ComponentId id, ComponentImages from)
		{
			//TODO
		}
	}
}

