using System;
using CapStore.Domain.Makers;

namespace CapStore.ApplicationServices.Makers.Data
{
	/// <summary>
	/// 登録完了後のIDが付与されたメーカーデータ
	/// </summary>
	public class RegistryMakerDataDto
	{
		public RegistryMakerDataDto(Maker from)
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

