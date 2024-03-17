using System;
using System.Text.RegularExpressions;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Akizukies.Catalogs
{
	/// <summary>
	/// 秋月電子のカタログページURL
	/// </summary>
	public class AkizukiCatalogPageUrl
	{

		private readonly string PATTERN = $"{AkizukiPageUrlDomain.Value}/catalog/g/g\\d+/";

		private readonly string _url;

		private readonly CatalogId _catalogId;

		public AkizukiCatalogPageUrl(AkizukiPageUrl url)
		{
			if (url == null)
			{
				throw new ValidationArgumentNullException("URLは必須です");
			}

			if (Regex.IsMatch(url.Value, PATTERN) == false)
			{
				throw new ValidationArgumentException("秋月電子のカタログURLではありません.製品ページのURLを指定してください");
			}

			_url = url.Value;

			int startIndex = $"{AkizukiPageUrlDomain.Value}/catalog/g/g".Length;
			int length = (_url.Length - startIndex) - 1;
			_catalogId = new CatalogId(_url.Substring(startIndex, length));
		}

		public AkizukiCatalogPageUrl(CatalogId catalogId)
		{
			if (catalogId == null)
			{
				throw new ValidationArgumentNullException("カタログIDは必須です");
			}

			_catalogId = catalogId;
			_url = $"{AkizukiPageUrlDomain.Value}/catalog/g/g{_catalogId.Value}/";
		}

		/// <summary>
		/// 秋月電子のページURL
		/// </summary>
		public string Value => _url;

		/// <summary>
		/// カタログID(通販コード)
		/// </summary>
		public CatalogId CatalogId => _catalogId;

	}
}

