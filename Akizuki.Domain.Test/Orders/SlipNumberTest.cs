using Akizuki.Domain.Orders;
using CapStore.Domain.Shareds.Exceptions;

namespace Akizuki.Domain.Test.Orders;

public class SlipNumberTest
{
    [Theory(DisplayName = "伝票番号 失敗テスト")]
    [Trait("Category", "Akizuki")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-123)]
    [InlineData(1223)]
    public void FormatFailTest(Int64 value)
    {
        Assert.Throws<ValidationArgumentException>(() => new SlipNumber(value));
    }

    [Theory(DisplayName = "伝票番号成功テスト")]
    [Trait("Category", "Akizuki")]
    [InlineData(569321486926)]
    [InlineData(569321428620)]
    [InlineData(569321784743)]
    [InlineData(569321400211)]
    [InlineData(569320425015)]
    [InlineData(569320385152)]
    [InlineData(569321792314)]
    [InlineData(569320686222)]
    public void SuccessTest(Int64 value)
    {
        SlipNumber slipNumber = new SlipNumber(value);
        Assert.Equal(value, slipNumber.Value);
    }
}
