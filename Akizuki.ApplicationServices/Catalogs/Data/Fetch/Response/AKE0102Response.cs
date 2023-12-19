using CapStore.Domain.Shareds;

namespace Akizuki.ApplicationService.Catalogs;

public class AKE0102Response : FetchAkizukiPageErrorResponseDataDto
{
    public AKE0102Response()
    : base(new Error(new ErrorCode("AKE0102"),
                new ErrorMessage("秋月電子のページ解析に失敗しました")))
    {
    }
}
