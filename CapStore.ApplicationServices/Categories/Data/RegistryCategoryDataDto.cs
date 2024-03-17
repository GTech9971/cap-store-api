using System;
using CapStore.Domains.Categories;

namespace CapStore.ApplicationServices.Categories.Data
{
	/// <summary>
	/// カテゴリー登録後のIDが付与されたカテゴリーデータ
	/// </summary>
	public class RegistryCategoryDataDto
	{
		public RegistryCategoryDataDto(Category from)
		{
			Id = from.Id.Value;
			Name = from.Name.Value;
			Image = from.Image?.Value;
		}

		public int Id { get; }

		public string Name { get; }

		public string? Image { get; }
	}
}

