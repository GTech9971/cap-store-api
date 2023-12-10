using System;
using CapStore.Domain.Components;

namespace CapStore.ApplicationServices.Components.Data.Registry
{
	/// <summary>
	/// 電子部品登録後のIDが付与された電子部品データ
	/// </summary>
	public class RegistryComponentDataDto
	{
		public RegistryComponentDataDto(Component from)
		{
			Id = from.Id.Value;
		}

		public int Id { get; }
	}
}

