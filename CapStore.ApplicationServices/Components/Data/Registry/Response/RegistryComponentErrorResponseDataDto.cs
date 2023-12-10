using System;
using CapStore.ApplicationServices.Shareds;

namespace CapStore.ApplicationServices.Components.Data.Registry.Response
{
	/// <summary>
	/// 電子部品登録エラーレスポンス
	/// </summary>
	public class RegistryComponentErrorResponseDataDto : RegistryComponentResponseDataDto
	{

		public RegistryComponentErrorResponseDataDto(Error error) : base(null)
		{
			Success = false;
			Errors = new List<Error>() { error };
		}

		public RegistryComponentErrorResponseDataDto(IEnumerable<Error> errors) : base(null)
		{
			Success = false;
			Errors = new List<Error>(errors);
		}
	}
}