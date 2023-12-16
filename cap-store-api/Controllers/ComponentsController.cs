using System;
using CapStore.ApplicationServices.Components;
using Microsoft.AspNetCore.Mvc;
using CapStore.ApplicationServices.Components.Exceptions;
using CapStore.ApplicationServices.Components.Data.Registry.Response;
using CapStore.ApplicationServices.Components.Data.Registry;
using CapStore.ApplicationServices.Components.Data.Fetch;
using CapStore.ApplicationServices.Components.Data.Fetch.Response;
using CapStore.ApplicationServices;

namespace cap_store_api.Controllers
{
	[ApiController]
	[Route("/api/v1/components")]
	public class ComponentsController : ControllerBase
	{
		private readonly ComponentsApplicationService _applicationService;

		public ComponentsController(ComponentsApplicationService applicationService)
		{
			_applicationService = applicationService;
		}

		/// <summary>
		/// 電子部品登録API
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> RegistryComponent([FromBody] RegistryComponentRequestDataDto request)
		{
			RegistryComponentResponseDataDto response;
			try
			{
				RegistryComponentDataDto data = await _applicationService.Registry(request);
				response = new RegistryComponentSuccessResponseDataDto(data);
			}
			catch (AlreadyExistComponentException)
			{
				response = new CRE0101Response();
			}
			catch (NotFoundCategoryIdException ex)
			{
				response = new CRE0102Response(ex);
			}
			catch (NotFoundMakerIdException ex)
			{
				response = new CRE0103Response(ex);
			}

			JsonResult result = new JsonResult(response)
			{
				StatusCode = (int?)response.StatusCode
			};
			return result;
		}

		/// <summary>
		/// 電子部品取得API
		/// </summary>
		/// <param name="pageIndex">ページ数 0~</param>
		/// <param name="pageSize">ページサイズ</param>
		/// <param name="sortColumn">TODO</param>
		/// <param name="sortOrder">TODO</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> FetchComponents(int pageIndex = 0,
														 int pageSize = 10)
		{
			FetchComponentListDataDto components =
				await _applicationService.FetchComponents(pageIndex,
														  pageSize,
														  null,
														  null);

			FetchComponentsSuccessPageResponseDataDto response =
			 new FetchComponentsSuccessPageResponseDataDto(components, pageIndex, pageSize);

			JsonResult result = new JsonResult(response)
			{
				StatusCode = (int?)response.StatusCode
			};

			return result;

		}
	}
}

