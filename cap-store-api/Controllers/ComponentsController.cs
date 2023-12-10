using System;
using CapStore.ApplicationServices.Components;
using Microsoft.AspNetCore.Mvc;
using CapStore.Domain.Components;
using CapStore.Domain.Categories;
using CapStore.Domain.Makers;
using CapStore.Domain.Shareds;
using CapStore.ApplicationServices.Components.Exceptions;
using CapStore.ApplicationServices.Components.Data.Registry.Response;
using CapStore.ApplicationServices.Components.Data.Registry;
using CapStore.Domain.Components.Services;

namespace cap_store_api.Controllers
{
	[ApiController]
	[Route("/api/v1/components")]
	public class ComponentsController:ControllerBase
	{
		private readonly ComponentsApplicationService _applicationService;
		private readonly IComponentRepository _componentRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMakerRepository _makerRepository;

		public ComponentsController(IComponentRepository componentRepository,
			ICategoryRepository categoryRepository,
			IMakerRepository makerRepository)
		{
			_componentRepository = componentRepository;
			_categoryRepository = categoryRepository;
			_makerRepository = makerRepository;

			_applicationService = new ComponentsApplicationService(_componentRepository,
				new ComponentService(_componentRepository));
		}

		/// <summary>
		/// 電子部品登録API
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// <exception cref="AlreadyExistComponentException"></exception>
		[HttpPost]
		public async Task<RegistryComponentResponseDataDto> RegistryComponent([FromBody]RegistryComponentRequestDataDto request)
		{
			try
			{
				CategoryId categoryId = new CategoryId(request.CategoryId);
				Category? category = await _categoryRepository.Fetch(categoryId);
				if(category == null)
				{
					return new CRE0102Response(categoryId);
				}


                MakerId makerId = new MakerId(request.MakerId);
                Maker? maker = await _makerRepository.Fetch(makerId);
				if(maker == null)
				{
					return new CRE0103Response(makerId);
				}

				ComponentImageList imageList = request.Images == null
					? ComponentImageList.Empty()
					: new ComponentImageList(request.Images.Select(x => {
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

				RegistryComponentDataDto data = await _applicationService.Registry(component);

				return new RegistryComponentSuccessResponseDataDto(data);
            }
			catch(AlreadyExistComponentException ex)
			{
				return new CRE0101Respose();
			}
            catch(Exception ex)
			{
				throw;
			}
		}
	}
}

