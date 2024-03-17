using System;
namespace CapStore.Domains.Makers.Services
{
	/// <summary>
	/// メーカードメインサービス
	/// </summary>
	public class MakerService
	{
		private readonly IMakerRepository _repository;

		public MakerService(IMakerRepository repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// メーカーがレポジトリに存在するか
		/// </summary>
		/// <param name="makerName">メーカー名をもとに確認する</param>
		/// <returns></returns>
		public async Task<bool> Exists(MakerName makerName)
		{
			var maker = await _repository.Fetch(makerName);
			return maker != null;
		}
	}
}

