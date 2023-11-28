using Akizuki.ApplicationService.Catalogs.Data;
using Akizuki.ApplicationService.Catalogs.Exceptions;
using Akizuki.Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <exception cref="FetchAkizukiCatalogPageException"></exception>
        public async Task<FetchAkizukiPageDataDto> FetchAkizukiPage(AkizukiCatalogPageUrl url)
        {
            try
            {
                AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(url);
                return new FetchAkizukiPageDataDto(akizukiPage);
            }
            catch (Exception ex)
            {
                throw new FetchAkizukiCatalogPageException(url, ex.Message);
            }
            
        }

    }
}
