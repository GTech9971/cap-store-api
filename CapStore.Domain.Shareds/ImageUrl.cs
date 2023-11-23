using System;
using System.Text.RegularExpressions;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Shareds
{
	/// <summary>
	/// 画像URL
	/// </summary>
	public class ImageUrl
	{

		private const string PATTERN = @"(https?)(:\/\/[-_.!~*\'()a-zA-Z0-9;\/?:\@&=+\$,%#]+)\.(jpg|jpeg|gif|png)";

        private readonly string _imageUrl;

		public ImageUrl(string imageUrl)
		{
			if (string.IsNullOrWhiteSpace(imageUrl))
			{
				throw new ValidationArgumentNullException("URLがからです");
			}

			if(Regex.IsMatch(imageUrl, PATTERN) == false)
			{
				throw new ValidationArgumentException("入力されたURLが画像URLのフォーマットではありません。");
			}


			_imageUrl = imageUrl;
		}


		/// <summary>
		/// 画像URL
		/// </summary>
		public string Value => _imageUrl;
	}
}

