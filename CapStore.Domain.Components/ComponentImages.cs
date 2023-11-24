using System;
using CapStore.Domain.Shareds;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components
{
	/// <summary>
	/// 電子部品画像URLリスト
	/// </summary>
	public class ComponentImages
	{
		/// <summary>
		/// 空の画像リスト
		/// </summary>
		/// <returns></returns>
		public static ComponentImages Empty()
		{
			return new ComponentImages();
		}

		private readonly List<ImageUrl> _imageUrls;

		public ComponentImages()
		{
			_imageUrls = new List<ImageUrl>();			
		}

        public ComponentImages(IEnumerable<ImageUrl> images)
        {
            _imageUrls = new List<ImageUrl>(images);
        }


        public ComponentImages Add(ImageUrl imageUrl)
		{
			if (_imageUrls.Contains(imageUrl))
			{
				throw new ValidationException("画像URLが重複しています");
			}

			var temps = new List<ImageUrl>(_imageUrls);			
			temps.Add(imageUrl);
			return new ComponentImages(temps);
		}

		public IReadOnlyCollection<ImageUrl> AsList()
		{
			return _imageUrls.AsReadOnly();
		}


		public bool Any()
		{
			return _imageUrls.Any();
		}
	}
}

