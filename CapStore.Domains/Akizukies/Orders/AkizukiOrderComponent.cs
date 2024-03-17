using CapStore.Domains.Akizukies.Catalogs;
using CapStore.Domains.Components;
using CapStore.Domains.Inventories;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Akizukies.Orders;

/// <summary>
/// 秋月電子での注文詳細内容
/// </summary>
public class AkizukiOrderComponent
{
    private readonly Quantity _quantity;
    private readonly Unit _unit;
    private readonly CatalogId _catalogId;
    private readonly ComponentId _componentId;

    public AkizukiOrderComponent(Quantity quantity,
                                Unit unit,
                                CatalogId catalogId,
                                ComponentId componentId)
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

        _quantity = quantity;
        _unit = unit;
        _catalogId = catalogId;
        _componentId = componentId;
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
    /// 新しい電子部品IDを適用する
    /// </summary>
    /// <param name="componentId"></param>
    /// <returns></returns>
    public AkizukiOrderComponent ApplyNewComponentId(ComponentId componentId)
    {
        return new AkizukiOrderComponent(_quantity, _unit, _catalogId, componentId);
    }
}
