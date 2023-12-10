using System;
using CapStore.ApplicationServices.Components.Data.Registry;
using CapStore.ApplicationServices.Components.Exceptions;
using CapStore.Domain.Components;
using CapStore.Domain.Components.Services;

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

	}
}

