using CapStore.Domain.Shareds;

namespace Akizuki.ApplicationService.Catalogs;

/// <summary>
/// 秋月電子のカタログページから電子部品取得エラーレスポンス
/// </summary>
public class FetchAkizukiPageErrorResponseDataDto : FetchAkizukiPageResponseDataDto
{
    public FetchAkizukiPageErrorResponseDataDto(Error error) : base(null)
    {
        Success = false;
        Errors = new List<Error>() { error };
    }

    public FetchAkizukiPageErrorResponseDataDto(IEnumerable<Error> errors) : base(null)
    {
        Success = false;
        Errors = new List<Error>(errors);
    }
}
