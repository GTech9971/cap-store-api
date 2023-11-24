using System;
using System.Text.RegularExpressions;
using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Catalogs
{
	/// <summary>
	/// 秋月電子のカタログページURL
	/// </summary>
	public class AkizukiCatalogPageUrl
	{

		private readonly string PATTERN = $"{AkizukiPageUrlDomain.Value}/catalog/g/g[A-Z]-\\d+/";

        private readonly string _url;

		public AkizukiCatalogPageUrl(AkizukiPageUrl url)
		{
			if (url == null)
			{
				throw new ValidationArgumentNullException("URLは必須です");
			}

			if(Regex.IsMatch(url.Value, PATTERN) == false)
			{
				throw new ValidationArgumentException("秋月電子のカタログURLではありません.製品ページのURLを指定してください");
			}

			_url = url.Value;
		}

		/// <summary>
		/// 秋月電子のページURL
		/// </summary>
		public string Value => _url;
	}
}

