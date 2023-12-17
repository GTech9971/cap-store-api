using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Orders;

/// <summary>
/// 秋月電子で注文した日時
/// </summary>
public class OrderDate
{
    private readonly DateOnly MIN_DATE = DateOnly.Parse("2001/1/1");
    private readonly DateOnly _value;

    public OrderDate(DateOnly date)
    {
        if (date < MIN_DATE)
        {
            throw new ValidationArgumentException("注文日が不正です");
        }
        _value = date;
    }

    /// <summary>
    /// 注文日
    /// </summary>
    public DateOnly Value => _value;

    public override string ToString()
    {
        return $"{_value.Year}年{_value.Month}月{_value.Day}日";
    }

}
