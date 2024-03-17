using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Components;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Akizukies.Orders;

/// <summary>
/// 秋月電子の電子部品情報
/// </summary>
public class AkizukiComponent
{
    private readonly CatalogId _catalogId;
    private readonly ComponentId _componentId;
    private readonly ComponentName _componentName;

    public AkizukiComponent(CatalogId catalogId,
                            ComponentId componentId,
                            ComponentName componentName)
    {
        if (catalogId == null)
        {
            throw new ValidationArgumentNullException("カタログIDは必須です");
        }

        if (componentId == null)
        {
            throw new ValidationArgumentNullException("電子部品IDは必須です");
        }

        if (componentName == null)
        {
            throw new ValidationArgumentNullException("電子部品名は必須です");
        }

        _catalogId = catalogId;
        _componentId = componentId;
        _componentName = componentName;
    }

    /// <summary>
    /// カタログID
    /// </summary>
    public CatalogId CatalogId => _catalogId;

    /// <summary>
    /// 電子部品ID
    /// </summary>
    public ComponentId ComponentId => _componentId;

    /// <summary>
    /// 電子部品名
    /// </summary>
    public ComponentName ComponentName => _componentName;
}
