using System;
using System.Text.RegularExpressions;
using CapStore.Domains.Shareds;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Akizukies.Catalogs
{
	/// <summary>
	/// 秋月電子の画像URL
	/// </summary>
	public class AkizukiImageUrl
	{
		public static readonly string PATTERN = $"{AkizukiPageUrlDomain.Value}/img/goods/[A-Z0-9]/1\\d{{5}}.(jpg|jpeg|gif|png)$";

		private readonly ImageUrl _url;

		public AkizukiImageUrl(AkizukiPageUrl url)
		{
			if (url == null)
			{
				throw new ValidationArgumentNullException("URLは必須です");
			}

			if (Regex.IsMatch(url.Value, PATTERN) == false)
			{
				throw new ValidationArgumentException("秋月電子の画像URLのフォーマットが不正です");
			}

			_url = new ImageUrl(url.Value);
		}

		/// <summary>
		/// 秋月電子の画像URL
		/// </summary>
		public string Value => _url.Value;

		/// <summary>
		/// 画像URL
		/// </summary>
		public ImageUrl ImageUrl => _url;
	}
}

