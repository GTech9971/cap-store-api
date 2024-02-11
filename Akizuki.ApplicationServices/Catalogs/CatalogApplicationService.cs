﻿using Akizuki.ApplicationService.Catalogs.Data.Fetch;
using Akizuki.Domain.Catalogs;
using CapStore.Domain.Categories;
using CapStore.Domain.Components;
using CapStore.Domain.Makers;

namespace Akizuki.ApplicationService.Catalogs
{
    /// <summary>
    /// カタログのアプリケーションサービス
    /// </summary>
    public class CatalogApplicationService
    {
        private readonly IAkizukiPageRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMakerRepository _makerRepository;

        public CatalogApplicationService(IAkizukiPageRepository repository,
                                        ICategoryRepository categoryRepository,
                                        IMakerRepository makerRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _makerRepository = makerRepository;
        }

        /// <summary>
        /// 秋月電子のカタログページURLから電子部品情報を取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="AkizukiPageHtmlParseException"></exception>
        /// <exception cref="AkizukiCatalogIdUnAvailableException"></exception>
        public async Task<FetchAkizukiPageDataDto> FetchComponentFromAkizukiCatalogIdAsync(string catalogIdStr)
        {
            CatalogId catalogId = new CatalogId(catalogIdStr);
            AkizukiPage akizukiPage = await _repository.FetchAkizukiPageAsync(catalogId);

            Category? foundCategory = await _categoryRepository.Fetch(akizukiPage.Component.Category.Name);
            if (foundCategory == null)
            {
                foundCategory = await _categoryRepository.Save(akizukiPage.Component.Category);
            }
            Maker? foundMaker = await _makerRepository.Fetch(akizukiPage.Component.Maker.Name);
            if (foundMaker == null)
            {
                foundMaker = await _makerRepository.Save(akizukiPage.Component.Maker);
            }

            AkizukiPage applyId = new AkizukiPage(akizukiPage.Url,
                new Component(
                    akizukiPage.Component.Id,
                    akizukiPage.Component.Name,
                    akizukiPage.Component.ModelName,
                    akizukiPage.Component.Description,
                    foundCategory,
                    foundMaker,
                    akizukiPage.Component.Images
                ));

            return new FetchAkizukiPageDataDto(applyId);
        }
    }
}
