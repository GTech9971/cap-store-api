using System.Net;
using Akizuki.ApplicationService.Catalogs.Data.Fetch;

namespace Akizuki.ApplicationService.Catalogs;

/// <summary>
/// 秋月電子から電子部品情報取得成功レスポンス
/// </summary>
public class FetchAkizukiPageSuccessResponseDataDto
: FetchAkizukiPageResponseDataDto
{
    public FetchAkizukiPageSuccessResponseDataDto(FetchAkizukiPageDataDto dto) : base(dto)
    {
        Success = true;
        StatusCode = HttpStatusCode.OK;
    }
}
