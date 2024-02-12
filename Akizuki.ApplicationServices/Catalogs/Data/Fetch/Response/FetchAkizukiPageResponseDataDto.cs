using Akizuki.ApplicationService.Catalogs.Data.Fetch;
using CapStore.Domain.Shareds.Responses;

namespace Akizuki.ApplicationService.Catalogs;

/// <summary>
/// 秋月電子から電子部品を取得したレスポンス
/// </summary>
public class FetchAkizukiPageResponseDataDto
: BaseResponse<FetchAkizukiPageDataDto<FetchCategoryDataDto, FetchMakerDataDto>>
{
    public FetchAkizukiPageResponseDataDto(FetchAkizukiPageDataDto<FetchCategoryDataDto, FetchMakerDataDto>? dto)
    {
        Data = dto != null
        ? new List<FetchAkizukiPageDataDto<FetchCategoryDataDto, FetchMakerDataDto>>() { dto }
        : null;
    }
}
