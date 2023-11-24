using System;
using System.Text.RegularExpressions;
using CapStore.Domain.Shareds;
using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Catalogs
{
	/// <summary>
	/// 秋月電子の画像URL
	/// </summary>
	public class AkizukiImageUrl
	{
		private readonly string PATTERN = $"{AkizukiPageUrlDomain.Value}/img/goods/[A-Z0-9]/[A-Z]-\\d{{5}}.(jpg|jpeg|gif|png)$";

		private readonly ImageUrl _url;

		public AkizukiImageUrl(AkizukiPageUrl url)
		{
			if (url == null)
			{
				throw new ValidationArgumentNullException("URLは必須です");
			}

			if(Regex.IsMatch(url.Value, PATTERN) == false)
			{
				throw new ValidationArgumentException("秋月電子の画像URLのフォーマットが不正です");
			}

			_url = new ImageUrl(url.Value);
		}

		/// <summary>
		/// 秋月電子の画像URL
		/// </summary>
		public string Value => _url.Value;
	}
}

