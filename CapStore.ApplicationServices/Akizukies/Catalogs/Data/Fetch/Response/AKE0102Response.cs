using CapStore.Domains.Shareds;

namespace CapStore.ApplicationServices.Akizukies.Catalogs.Data.Fetch.Response;

public class AKE0102Response : FetchAkizukiPageErrorResponseDataDto
{
    public AKE0102Response()
    : base(new Error(new ErrorCode("AKE0102"),
                new ErrorMessage("秋月電子のページ解析に失敗しました")))
    {
    }
}
