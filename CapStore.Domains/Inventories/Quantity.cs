using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Inventories;

/// <summary>
/// 個数
/// </summary>
public class Quantity
{

    private readonly int _quantity;

    public Quantity(int quantity)
    {
        if (quantity < 0)
        {
            throw new ValidationException("個数がマイナスです");
        }

        _quantity = quantity;
    }

    /// <summary>
    /// 個数
    /// </summary>
    public int Value => _quantity;

}
