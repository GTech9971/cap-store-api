using System;
using CapStore.Domain.Shareds;
using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Components
{
	/// <summary>
	/// 電子部品画像URLリスト
	/// </summary>
	public class ComponentImageList
	{
		/// <summary>
		/// 空の画像リスト
		/// </summary>
		/// <returns></returns>
		public static ComponentImageList Empty()
		{
			return new ComponentImageList();
		}

		private readonly List<ComponentImage> _imageUrls;

		public ComponentImageList()
		{
			_imageUrls = new List<ComponentImage>();			
		}

		public ComponentImageList(ComponentImage image)
		{
			_imageUrls = new List<ComponentImage>() { image };
		}

        public ComponentImageList(IEnumerable<ComponentImage> images)
        {
            _imageUrls = new List<ComponentImage>(images);
        }


        public ComponentImageList Add(ComponentImage imageUrl)
		{
			if (_imageUrls.Contains(imageUrl))
			{
				throw new ValidationException("画像URLが重複しています");
			}

			var temps = new List<ComponentImage>(_imageUrls);			
			temps.Add(imageUrl);
			return new ComponentImageList(temps);
		}

		public IReadOnlyCollection<ComponentImage> AsList()
		{
			return _imageUrls.AsReadOnly();
		}


		public bool Any()
		{
			return _imageUrls.Any();
		}

		public int Count
		{
			get { return _imageUrls.Count; }
		}
	}
}

