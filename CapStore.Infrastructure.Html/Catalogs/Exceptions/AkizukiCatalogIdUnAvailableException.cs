using CapStore.Domains.Akizukies.Catalogs;

namespace CapStore.Infrastructure.Html.Catalogs.Exceptions;

/// <summary>
/// 秋月電子でカタログIDが利用できない例外
/// </summary>
public class AkizukiCatalogIdUnAvailableException : Exception
{
    public AkizukiCatalogIdUnAvailableException(CatalogId catalogId, string message) :
    base($"{message} - カタログID:{catalogId.Value}")
    { }
}
