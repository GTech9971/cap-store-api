using System;
using CapStore.ApplicationServices.Components.Data.Registry;
using CapStore.ApplicationServices.Components.Exceptions;
using CapStore.Domain.Components;
using CapStore.Domain.Components.Services;
using Microsoft.EntityFrameworkCore;
using CapStore.ApplicationServices.Components.Data.Fetch;

namespace CapStore.ApplicationServices.Components
{
	/// <summary>
	/// 電子部品に関するアプリケーションサービス
	/// </summary>
	public class ComponentsApplicationService
	{

		private readonly IComponentRepository _repository;
		private readonly ComponentService _service;

		public ComponentsApplicationService(IComponentRepository repository,
											ComponentService service)
		{
			_repository = repository;
			_service = service;
		}


		/// <summary>
		/// 電子部品を登録する
		/// </summary>
		/// <param name="component">電子部品</param>
		/// <returns></returns>
		/// <exception cref="AlreadyExistComponentException"></exception>
		public async Task<RegistryComponentDataDto> Registry(Component component)
		{
			if(await _service.Exists(component.Id))
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

