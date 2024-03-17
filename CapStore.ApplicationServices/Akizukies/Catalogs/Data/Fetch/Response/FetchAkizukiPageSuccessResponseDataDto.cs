using System.Net;

namespace CapStore.ApplicationServices.Akizukies.Catalogs.Data.Fetch.Response;

/// <summary>
/// 秋月電子から電子部品情報取得成功レスポンス
/// </summary>
public class FetchAkizukiPageSuccessResponseDataDto
: FetchAkizukiPageResponseDataDto
{
    public FetchAkizukiPageSuccessResponseDataDto(FetchAkizukiPageDataDto<FetchCategoryDataDto, FetchMakerDataDto> dto) : base(dto)
    {
        Success = true;
        StatusCode = HttpStatusCode.OK;
    }
}
