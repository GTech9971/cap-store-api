using System;
using CapStore.Domain.Shareds.Responses;

namespace CapStore.ApplicationServices.Components.Data.Registry.Response
{
	/// <summary>
	/// 電子部品登録レスポンス
	/// </summary>
	public class RegistryComponentResponseDataDto
		: BaseResponse<RegistryComponentDataDto>
	{
		public RegistryComponentResponseDataDto(RegistryComponentDataDto? dto)
		{
			Data = dto != null
				? new List<RegistryComponentDataDto>() { dto }
				: null;
		}
	}
}