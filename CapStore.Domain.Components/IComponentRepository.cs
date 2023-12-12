using System;
namespace CapStore.Domain.Components
{
	/// <summary>
	/// 電子部品の永続化を行うレポジトリ
	/// </summary>
	public interface IComponentRepository
	{

		public Task<Component?> Fetch(ComponentId componentId);
		public Task<Component?> Fetch(ComponentName componentName);

		public Task<Component?> Fetch(ComponentModelName modelName);

		public IQueryable<Component> FetchAll();
		public Task<int> CountAsync();

		public Task<Component> Save(Component component);
	}
}

