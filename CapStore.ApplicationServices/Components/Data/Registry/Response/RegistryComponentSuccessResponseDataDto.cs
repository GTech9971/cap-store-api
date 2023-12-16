using System;
using System.Net;
namespace CapStore.ApplicationServices.Components.Data.Registry.Response
{
    /// <summary>
    /// 電子部品登録成功レスポンス
    /// </summary>
    public class RegistryComponentSuccessResponseDataDto
        : RegistryComponentResponseDataDto
    {
        public RegistryComponentSuccessResponseDataDto(RegistryComponentDataDto dto) : base(dto)
        {
            Success = true;
            StatusCode = HttpStatusCode.OK;
        }
    }
}

