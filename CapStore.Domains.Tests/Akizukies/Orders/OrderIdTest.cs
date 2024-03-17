using CapStore.Domains.Akizukies.Orders;
using CapStore.Domains.Shareds.Exceptions;

namespace CapStore.Domains.Tests.Akizukies.Orders;

public class OrderIdTest
{

    [Theory(DisplayName = "注文ID nullテスト")]
    [Trait("Category", "Akizuki")]
    [InlineData(null)]
    [InlineData("")]
    public void NullTest(string? orderIdStr)
    {
        Assert.Throws<ValidationArgumentNullException>(() => new OrderId(orderIdStr));
    }

    [Theory(DisplayName = "注文ID nullテスト")]
    [Trait("Category", "Akizuki")]
    [InlineData("AAA")]
    [InlineData("E230617-031873")]
    [InlineData("E230617-031873-0")]
    public void FormatFailTest(string orderIdStr)
    {
        Assert.Throws<ValidationArgumentException>(() => new OrderId(orderIdStr));
    }

    [Theory(DisplayName = "注文ID成功テスト")]
    [Trait("Category", "Akizuki")]
    [InlineData("E230617-031873-01")]
    [InlineData("E230607-009435-01")]
    [InlineData("E220706-100082-01")]
    [InlineData("E220425-114327-01")]
    [InlineData("E211028-104705-01")]
    [InlineData("E211021-107294-01")]
    [InlineData("E210705-044241-01")]
    [InlineData("E201202-117662-01")]
    public void SuccessTest(string orderIdStr)
    {
        OrderId orderId = new OrderId(orderIdStr);
        Assert.Equal(orderIdStr, orderId.Value);
    }
}
