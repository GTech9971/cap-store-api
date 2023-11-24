using System;
namespace Akizuki.Domain.Catalogs
{
	/// <summary>
	/// 秋月電子ページのレポジトリ
	/// </summary>
	public interface IAzikzukiPageRepository
	{

		/// <summary>
		/// 秋月電子のページを取得する
		/// </summary>
		/// <param name="url">秋月電子のページURL</param>
		/// <returns></returns>
		Task<AkizukiPage> FetchAkizukiPageAsync(AkizukiCatalogPageUrl url);
	}
}

