using System;
using System.Reflection.Metadata.Ecma335;
namespace CapStore.Domain.Components.Services
{
	/// <summary>
	/// 電子部品に関するドメインサービス
	/// </summary>
	public class ComponentService
	{

		private readonly IComponentRepository _repository;

		public ComponentService(IComponentRepository repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// 電子部品が存在するか調べる
		/// </summary>
		/// <param name="component">電子部品</param>
		/// <returns>true:存在する, false:存在しない</returns>
		public async Task<bool> Exists(Component component)
		{
			Component? existsId = await _repository.Fetch(component.Id);
			Component? existsModelName = await _repository.Fetch(component.ModelName);
			return existsId != null && existsModelName != null;
		}

		/// <summary>
		/// 電子部品が存在するか調べる
		/// </summary>
		/// <param name="componentId">電子部品ID</param>
		/// <returns>true:存在する, false:存在しない</returns>
		public async Task<bool> Exists(ComponentId componentId)
		{
			Component? existsId = await _repository.Fetch(componentId);
			return existsId != null;
		}

		/// <summary>
		/// 電子部品IDが全て存在するか調べる
		/// </summary>
		/// <param name="componentIdList">電子部品IDリスト</param>
		/// <returns>true:存在する, false:存在しない</returns>
		public async Task<bool> ExistsAll(IEnumerable<ComponentId> componentIdList)
		{
			foreach (ComponentId componentId in componentIdList)
			{
				if (await Exists(componentId) == false)
				{
					return false;
				}
			}
			return true;
		}

	}
}

