using System;
using CapStore.Domains.Makers;
using CapStore.ApplicationServices.Makers.Data;
using CapStore.ApplicationServices.Makers.Exceptions;
using CapStore.Domains.Makers.Services;
using CapStore.Domains.Shareds;

namespace CapStore.ApplicationServices.Makers
{
	/// <summary>
	/// メーカーのアプリケーションサービス
	/// </summary>
	public class MakerApplicationService
	{

		private readonly MakerService _service;
		private readonly IMakerRepository _repository;

		public MakerApplicationService(MakerService service,
										IMakerRepository repository)
		{
			_service = service;
			_repository = repository;
		}

		/// <summary>
		/// メーカー名で重複登録されていなければ、メーカー情報を登録する
		/// </summary>
		/// <param name="makerName">メーカー名</param>
		/// <param name="image">メーカー画像URL</param>
		/// <returns></returns>
		/// <exception cref="CanNotRegisterMakerException"></exception>
		public async Task<RegistryMakerDataDto> RegistryAsync(MakerName makerName,
														ImageUrl? image)
		{
			Maker maker = new Maker(MakerId.UnDetect(), makerName, image);

			if (await _service.Exists(maker.Name))
			{
				throw new CanNotRegisterMakerException(maker, "メーカーがすでに登録済みです");
			}

			Maker registeredMaker = await _repository.Save(maker);
			return new RegistryMakerDataDto(registeredMaker);
		}
	}
}

