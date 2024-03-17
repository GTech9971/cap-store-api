using System;
using System.Text.RegularExpressions;
using CapStore.Domains.Shareds.Exceptions;
namespace CapStore.Domains.Akizukies.Catalogs
{
	/// <summary>
	/// 秋月電子のページURL
	/// </summary>
	public class AkizukiPageUrl
	{

		private readonly string PATTERN = $"{AkizukiPageUrlDomain.Value}/";

		private readonly string _url;

		public AkizukiPageUrl()
		{
			_url = AkizukiPageUrlDomain.Value;
		}

		public AkizukiPageUrl(string url) : this()
		{
			if (string.IsNullOrWhiteSpace(url))
			{
				throw new ValidationArgumentNullException("URLは必須です");
			}

			//ドメインがないパスURLの場合
			if (url.Contains(AkizukiPageUrlDomain.Value) == false)
			{
				_url = url.Substring(0) == "/"
					? $"{AkizukiPageUrlDomain.Value}{url}"
					: $"{AkizukiPageUrlDomain.Value}{url}";
			}
			else
			{
				_url = url;
			}

			if (Regex.IsMatch(_url, PATTERN) == false)
			{
				throw new ValidationArgumentException("秋月電子のURLフォーマットではありません");
			}
		}

		/// <summary>
		/// 秋月電子のURL
		/// </summary>
		public string Value => _url;

	}
}

