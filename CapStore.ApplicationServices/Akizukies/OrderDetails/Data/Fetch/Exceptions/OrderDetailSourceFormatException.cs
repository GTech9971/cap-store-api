namespace CapStore.ApplicationServices.Akizukies.OrderDetails.Data.Fetch.Exceptions;

/// <summary>
/// 秋月電子の注文詳細のソースのフォーマットの例外
/// </summary>
public class OrderDetailSourceFormatException : Exception
{
    public OrderDetailSourceFormatException()
    : base($"ソースのフォーマットが不正です") { }
}
