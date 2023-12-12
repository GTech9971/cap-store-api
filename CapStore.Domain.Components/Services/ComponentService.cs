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
		/// <param name="componentId">電子部品ID</param>
		/// <returns>true:存在する, false:存在しない</returns>
		public async Task<bool> Exists(ComponentId componentId)
		{
			Component? component = await _repository.Fetch(componentId);
			return component != null;
		}

        /// <summary>
        /// 電子部品が存在するか調べる
        /// </summary>
        /// <param name="modelName">モデル名</param>
        /// <returns>true:存在する, false:存在しない</returns>
        public async Task<bool> Exists(ComponentModelName modelName)
		{
            Component? component = await _repository.Fetch(modelName);
            return component != null;
        }

	}
}

