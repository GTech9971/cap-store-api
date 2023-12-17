using Akizuki.Domain.Orders;
using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Test.Orders;

public class OrderDateTest
{


    [Theory(DisplayName = "注文日 失敗テスト")]
    [Trait("Category", "Akizuki")]
    [InlineData(1997, 1, 1)]
    [InlineData(2000, 12, 31)]
    public void FormatFailTest(int year, int month, int day)
    {
        DateOnly date = new DateOnly(year, month, day);
        Assert.Throws<ValidationArgumentException>(() => new OrderDate(date));
    }

    [Theory(DisplayName = "注文日成功テスト")]
    [Trait("Category", "Akizuki")]
    [InlineData(2001, 1, 1)]
    [InlineData(2023, 11, 1)]
    [InlineData(2011, 11, 5)]
    [InlineData(2061, 8, 1)]
    [InlineData(2023, 12, 11)]
    public void SuccessTest(int year, int month, int day)
    {
        DateOnly date = new DateOnly(year, month, day);
        OrderDate orderDate = new OrderDate(date);
        Assert.Equal($"{year}年{month}月{day}日", orderDate.ToString());
    }
}
