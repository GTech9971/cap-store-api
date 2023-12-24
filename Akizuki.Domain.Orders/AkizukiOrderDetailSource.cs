using System.Text;
using Akizuki.Domain.Orders.Exceptions;
using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Orders;

/// <summary>
/// 秋月電子の注文詳細のソース
/// </summary>
public class AkizukiOrderDetailSource
{
    public const int MIN_SIZE_KB = 1024;
    public const int MAX_SIZE_KB = 100 * 1024;

    private readonly string _source;

    public AkizukiOrderDetailSource(string source)
    {
        if (source == null)
        {
            throw new ValidationArgumentNullException("ソースは必須です");
        }

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        byte[] data = Encoding.GetEncoding("SHIFT_JIS").GetBytes(source);

        if (data.Length > MAX_SIZE_KB)
        {
            throw new OrderDetailSourceSizeException(data.Length);
        }

        if (data.Length < MIN_SIZE_KB)
        {
            throw new OrderDetailSourceSizeException(data.Length);
        }

        _source = source;
    }


    /// <summary>
    /// 秋月電子の注文詳細のソース
    /// </summary>
    public string Value => _source;
}
