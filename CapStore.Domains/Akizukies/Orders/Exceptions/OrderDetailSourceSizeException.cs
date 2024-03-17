namespace CapStore.Domains.Akizukies.Orders.Exceptions;

/// <summary>
/// 秋月電子の注文詳細ソースのサイズ例外
/// </summary>
public class OrderDetailSourceSizeException : Exception
{
    public OrderDetailSourceSizeException(long length) : base($"ソースが大きすぎるか、小さすぎます {length}") { }
}
