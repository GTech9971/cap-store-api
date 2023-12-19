using Akizuki.ApplicationService.Catalogs.Data.Fetch;
using Akizuki.Domain.Catalogs;

namespace Akizuki.ApplicationService.Catalogs
{
    /// <summary>
    /// カタログのアプリケーションサービス
    /// </summary>
    public class CatalogApplicationService
    {
        private readonly IAzikzukiPageRepository _repository;

        public CatalogApplicationService(IAzikzukiPageRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 秋月電子のカタログページURLから電子部品情報を取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="AkizukiPageHtmlParseException"></exception>
        public async Task<FetchAkizukiPageDataDto> FetchComponentFromAkizukiCatalogIdAsync(string catalogIdStr)
        {
            CatalogId catalogId = new CatalogId(catalogIdStr);
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(catalogId);
            return new FetchAkizukiPageDataDto(akizukiPage);
        }

    }
}
