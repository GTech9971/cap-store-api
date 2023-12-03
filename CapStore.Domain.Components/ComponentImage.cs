using System;
using CapStore.Domain.Shareds;

namespace CapStore.Domain.Components
{
    /// <summary>
    /// 電子部品画像URL
    /// </summary>
    public class ComponentImage
	{

		private readonly ComponentImageId _id;
		private readonly ComponentId _componentId;
		private readonly ImageUrl _imageUrl;

		/// <summary>
		/// IDが未定の電子部品画像URLモデルを生成する
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static ComponentImage UnDetectId(ImageUrl url)
		{
			return new ComponentImage(
				ComponentImageId.UnDetectId(),
				ComponentId.UnDetectId(),
				url);
		}


		public ComponentImage(ComponentImageId id,
								ComponentId componentId,
								ImageUrl imageUrl)
		{
			_id = id;
			_componentId = componentId;
			_imageUrl = imageUrl;
		}

		/// <summary>
		/// 電子部品画像UrlID
		/// </summary>
		public ComponentImageId ComponentImageId => _id;
		/// <summary>
		/// 電子部品ID
		/// </summary>
		public ComponentId ComponentId => _componentId;
		/// <summary>
		/// 電子部品画像Url
		/// </summary>
		public ImageUrl Image => _imageUrl;
	}
}

