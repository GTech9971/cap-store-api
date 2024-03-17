using System;
namespace CapStore.Domains.Akizukies.Catalogs
{
	/// <summary>
	/// 秋月電子ページのレポジトリ
	/// </summary>
	public interface IAkizukiPageRepository
	{

		/// <summary>
		/// 秋月電子のページを取得する
		/// </summary>
		/// <param name="url">秋月電子のページURL</param>
		/// <returns></returns>
		Task<AkizukiPage> FetchAkizukiPageAsync(AkizukiCatalogPageUrl url);

		/// <summary>
		/// 秋月電子のページを取得する
		/// </summary>
		/// <param name="catalogId">カタログID</param>
		/// <returns></returns>
		Task<AkizukiPage> FetchAkizukiPageAsync(CatalogId catalogId);
	}
}

