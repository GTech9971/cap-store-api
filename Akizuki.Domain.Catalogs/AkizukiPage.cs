using System;
using CapStore.Domain.Components;

namespace Akizuki.Domain.Catalogs
{
	/// <summary>
	/// 秋月電子のページ
	/// </summary>
	public class AkizukiPage
	{

		private readonly AkizukiCatalogPageUrl _url;
		private readonly Component _component;

		public AkizukiPage(AkizukiCatalogPageUrl url, Component component)
		{
			_url = url;
			_component = component;
		}

		/// <summary>
		/// ページのURL
		/// </summary>
		public AkizukiCatalogPageUrl Url => _url;

		/// <summary>
		/// 電子部品情報
		/// </summary>
		public Component Component => _component;
	}
}

