using System;
using CapStore.ApplicationServices.Components.Data.Registry;
using CapStore.ApplicationServices.Components.Exceptions;
using CapStore.Domain.Components;
using CapStore.Domain.Components.Services;
using Microsoft.EntityFrameworkCore;
using CapStore.ApplicationServices.Components.Data.Fetch;
using CapStore.Domain.Categories;
using CapStore.Domain.Makers;
using CapStore.Domain.Shareds;

namespace CapStore.ApplicationServices.Components
{
    /// <summary>
    /// 電子部品に関するアプリケーションサービス
    /// </summary>
    public class ComponentsApplicationService
    {

        private readonly IComponentRepository _repository;
        private readonly ComponentService _service;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMakerRepository _makerRepository;

        public ComponentsApplicationService(IComponentRepository repository,
                                            ComponentService service,
                                            IMakerRepository makerRepository,
                                            ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _service = service;
            _makerRepository = makerRepository;
            _categoryRepository = categoryRepository;
        }


        /// <summary>
        /// 電子部品を登録する
        /// </summary>
        /// <param name="component">電子部品</param>
        /// <returns></returns>
        /// <exception cref="AlreadyExistComponentException"></exception>
        /// <exception cref="NotFoundCategoryIdException"></exception>
        /// <exception cref="NotFoundMakerIdException"></exception>
        public async Task<RegistryComponentDataDto> Registry(RegistryComponentRequestDataDto request)
        {
            CategoryId categoryId = new CategoryId(request.CategoryId);
            Category? category = await _categoryRepository.Fetch(categoryId);
            if (category == null)
            {
                throw new NotFoundCategoryIdException(categoryId);
            }


            MakerId makerId = new MakerId(request.MakerId);
            Maker? maker = await _makerRepository.Fetch(makerId);
            if (maker == null)
            {
                throw new NotFoundMakerIdException(makerId);
            }

            ComponentImageList imageList = request.Images == null
                ? ComponentImageList.Empty()
                : new ComponentImageList(request.Images.Select(x =>
                {
                    return new ComponentImage(
                        ComponentImageId.UnDetectId(),
                        ComponentId.UnDetectId(),
                        new ImageUrl(x));
                }));


            Component component = new Component(
                ComponentId.UnDetectId(),
                new ComponentName(request.Name),
                new ComponentModelName(request.ModelName),
                new ComponentDescription(request.Description),
                category,
                maker,
                imageList);

            if (await _service.Exists(component))
            {
                throw new AlreadyExistComponentException(component, "電子部品がすでに存在するため登録できません");
            }

            Component registeredComponent = await _repository.Save(component);
            return new RegistryComponentDataDto(registeredComponent);
        }

        /// <summary>
        /// 電子部品を取得する
        /// </summary>
        /// <param name="pageIndex">取得ページ</param>
        /// <param name="pageSize">表示数</param>
        /// <param name="sortColumn">ソート</param>
        /// <param name="sortOrder">ソート順</param>
        /// <returns></returns>
        public async Task<FetchComponentListDataDto> FetchComponents(int pageIndex,
                                                                int pageSize,
                                                                string? sortColumn,
                                                                string? sortOrder)
        {
            IQueryable<Component> components = _repository.FetchAll();

            int count = await _repository.CountAsync();

            //if (!string.IsNullOrEmpty(sortColumn)
            //    && IsValidProperty(sortColumn))
            //{
            //    sortOrder = !string.IsNullOrEmpty(sortOrder)
            //        && sortOrder.ToUpper() == "ASC"
            //        ? "ASC"
            //        : "DESC";
            //    components = components.OrderBy(
            //        string.Format(
            //            "{0} {1}",
            //            sortColumn,
            //            sortOrder)
            //        );
            //}

            components = components
                .Skip(pageIndex * pageSize)
                .Take(pageSize);


            List<Component> data = await components.ToListAsync();

            return new FetchComponentListDataDto(data.Select(x => new FetchComponentDataDto(x)), count);
        }


        ///// <summary>
        ///// Checks if the given property name exists
        ///// to protect against SQL injection attacks
        ///// </summary>
        //public static bool IsValidProperty(
        //    string propertyName,
        //    bool throwExceptionIfNotFound = true)
        //{
        //    var prop = typeof(Component).GetProperty(
        //        propertyName,
        //        BindingFlags.IgnoreCase |
        //        BindingFlags.Public |
        //        BindingFlags.Static |
        //        BindingFlags.Instance);
        //    if (prop == null && throwExceptionIfNotFound)
        //    {
        //        throw new NotSupportedException($"ERROR: Property '{propertyName}' does not exist.");
        //    }
        //    return prop != null;
        //}

    }
}

