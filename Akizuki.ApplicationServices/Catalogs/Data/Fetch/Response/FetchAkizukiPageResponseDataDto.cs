using Akizuki.ApplicationService.Catalogs.Data.Fetch;
using CapStore.Domain.Shareds.Responses;

namespace Akizuki.ApplicationService.Catalogs;

/// <summary>
/// 秋月電子から電子部品を取得したレスポンス
/// </summary>
public class FetchAkizukiPageResponseDataDto
: BaseResponse<FetchAkizukiPageDataDto>
{
    public FetchAkizukiPageResponseDataDto(FetchAkizukiPageDataDto? dto)
    {
        Data = dto != null
        ? new List<FetchAkizukiPageDataDto>() { dto }
        : null;
    }
}
