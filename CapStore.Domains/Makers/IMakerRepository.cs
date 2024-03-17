using System;
namespace CapStore.Domains.Makers
{
	/// <summary>
	/// メーカーの永続化を行うレポジトリ
	/// </summary>
	public interface IMakerRepository
	{
		Task<Maker?> Fetch(MakerId makerId);
		Task<Maker?> Fetch(MakerName makerName);

		IAsyncEnumerable<Maker> FetchAll();

		Task<Maker> Save(Maker maker);
	}
}

