using System;
namespace CapStore.Domain.Makers
{
	/// <summary>
	/// メーカーの永続化を行うレポジトリ
	/// </summary>
	public interface IMakerRepository
	{
		Task<Maker?> Fetch(MakerId makerId);
		Task<Maker?> Fetch(MakerName makerName);

		Task<Maker> Save(Maker maker);
	}
}

