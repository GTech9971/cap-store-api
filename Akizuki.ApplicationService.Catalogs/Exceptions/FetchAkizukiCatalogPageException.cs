using Akizuki.Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akizuki.ApplicationService.Catalogs.Exceptions
{
    /// <summary>
    /// 秋月電子のカタログページの取得時の例外
    /// </summary>
    public class FetchAkizukiCatalogPageException : Exception
    {
        public FetchAkizukiCatalogPageException(AkizukiCatalogPageUrl url, string message)
         : base($"{message}:{url}")
        {
        }
    }
}
