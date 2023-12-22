using CapStore.Domain.Shareds.Exceptions;

namespace CapStore.Domain.Inventories;

/// <summary>
/// 在庫の単位
/// </summary>
public class Unit
{
    private readonly string _unit;

    public Unit(string unit)
    {
        if (string.IsNullOrWhiteSpace(unit))
        {
            throw new ValidationArgumentNullException("単位は必須です");
        }

        _unit = unit;
    }

    /// <summary>
    /// 在庫の単位
    /// </summary>
    public string Value => _unit;
}
