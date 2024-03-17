using System.Text.RegularExpressions;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Akizukies.Orders;

/// <summary>
/// 秋月電子で購入した際のID
/// </summary>
public class OrderId
{

    private const string PATTERN = @"^E\d{6}-\d{6}-\d{2}$";
    private readonly string _value;

    public OrderId(string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
        {
            throw new ValidationArgumentNullException("注文IDは必須です");
        }

        if (Regex.IsMatch(orderId, PATTERN) == false)
        {
            throw new ValidationArgumentException("注文IDのフォーマットが不正です");
        }

        _value = orderId;
    }

    /// <summary>
    /// 注文ID
    /// </summary>
    public string Value => _value;
}
