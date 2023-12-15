using System;
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

	}
}

