namespace CapStore.Domains.Components.Test;

using CapStore.Domains.Shareds.Exceptions;

public class ComponentIdTest
{
    private const string CATEGORY = "電子部品ID";

    [Theory]
    [Trait("Category", CATEGORY)]
    [InlineData(-1)]
    public void TestExceptions(int id)
    {
        Assert.Throws<ValidationArgumentException>(() =>
        {
            new ComponentId(id);
        });
    }

    [Theory]
    [Trait("Category", CATEGORY)]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(101)]
    [InlineData(int.MaxValue)]
    public void TestSuccess(int id)
    {
        var componentId = new ComponentId(id);
        Assert.Equal(id, componentId.Value);
    }
}
