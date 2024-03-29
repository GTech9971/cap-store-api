﻿using System;
using CapStore.Domains.Makers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CapStore.Domains.Shareds;

namespace CapStore.Infrastructure.Ef.Makers.Data
{
	/// <summary>
	/// efで使用するメーカーのデータ
	/// </summary>
	[Table("makers")]
	[Index(nameof(Name))]
	public class MakerData
	{
		/// <summary>
		/// 主キー
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }

		/// <summary>
		/// 重複不可
		/// </summary>
		[Column("name")]
		public string Name { get; set; } = null!;


		/// <summary>
		/// メーカー画像URL
		/// </summary>
		[DataType(DataType.ImageUrl)]
		[Column("image_url")]
		public string? Image { get; set; }

		public MakerData() { }

		public MakerData(Maker from)
		{
			if (from.Id.IsUnDetect == false)
			{
				Id = from.Id.Value;
			}

			Name = from.Name.Value;
			Image = from.Image?.Value;
		}

		public Maker ToModel()
		{
			return new Maker(
			  new MakerId(Id),
			  new MakerName(Name),
			  Image == null
			  ? null
			  : new ImageUrl(Image));
		}
	}
}

