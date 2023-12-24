using Akizuki.Domain.Catalogs;
using CapStore.Domain.Components;
using CapStore.Domain.Inventories;
using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Orders;

/// <summary>
/// 秋月電子での注文詳細内容
/// </summary>
public class AkizukiOrderComponent
{
    private readonly Quantity _quantity;
    private readonly Unit _unit;
    private readonly CatalogId _catalogId;
    private readonly ComponentId _componentId;
    private readonly ComponentName _componentName;

    private readonly bool _registered;

    public AkizukiOrderComponent(Quantity quantity,
                                 Unit unit,
                                CatalogId catalogId,
                                ComponentId componentId,
                                ComponentName componentName,
                                bool registered)
    {
        if (quantity == null)
        {
            throw new ValidationArgumentNullException("個数は必須です");
        }

        if (unit == null)
        {
            throw new ValidationArgumentNullException("単位は必須です");
        }

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

        _quantity = quantity;
        _unit = unit;
        _catalogId = catalogId;
        _componentId = componentId;
        _componentName = componentName;
        _registered = registered;
    }

    /// <summary>
    /// 個数
    /// </summary>
    public Quantity Quantity => _quantity;

    /// <summary>
    /// 単位
    /// </summary>
    public Unit Unit => _unit;

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

    /// <summary>
    /// true:電子部品マスターに登録ずみ, false:電子部品マスターに登録されていない
    /// </summary>
    public bool Registered => _registered;
}
