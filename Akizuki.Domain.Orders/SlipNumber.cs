using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Orders;

/// <summary>
/// 秋月電子で注文した伝票番号
/// </summary>
public class SlipNumber
{

    private const Int64 MIN_LENGTH = 12;
    private readonly Int64 _value;

    public SlipNumber(Int64 slipNumber)
    {
        if (slipNumber < 0)
        {
            throw new ValidationArgumentException("伝票番号が不正です");
        }

        if (slipNumber.ToString().Length < MIN_LENGTH)
        {
            throw new ValidationArgumentException("伝票番号の桁数が足りません");
        }

        _value = slipNumber;
    }

    /// <summary>
    /// 秋月電子で注文した伝票番号
    /// </summary>
    public Int64 Value => _value;
}
