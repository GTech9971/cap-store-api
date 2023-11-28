using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CapStore.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace CapStore.Infrastructure.Ef.Categories.Data
{
	/// <summary>
	/// efで使用するカテゴリーのデータモデル
	/// </summary>
	[Table("categories")]
	[Index(nameof(Name))]
	public class CategoryData
	{
		/// <summary>
		/// 主キー
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		[Column("id")]
		public int Id { get; set; }

		/// <summary>
		/// 重複不可
		/// </summary>
		[Required]
		[Column("name")]
		public string Name { get; set; } = null!;

		/// <summary>
		/// 画像URL
		/// </summary>
		[DataType(DataType.ImageUrl)]
		[Column("image_url")]
		public string? Image { get; set; }

		public CategoryData() { }

		public CategoryData(Category from)
		{
			if(from.Id.IsUnDetect == false)
			{
				Id = from.Id.Value;
			}
			Name = from.Name.Value;
			Image = from.Image?.Value;
		}
	}
}

