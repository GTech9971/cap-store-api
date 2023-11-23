namespace CapStore.Domain.Components.Test;
using CapStore.Domain.Shareds.Exceptions;

[TestClass]
public class ComponentIdTest
{
    private const string CATEGORY = "電子部品ID";

    [TestMethod]
    [TestCategory(CATEGORY)]
    [ExpectedException(typeof(ValidationArgumentException))]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(99)]
    public void TestExceptions(int id)
    {
        new ComponentId(id);
    }

    [TestMethod]
    [TestCategory(CATEGORY)]
    [DataRow(100)]
    [DataRow(101)]
    [DataRow(int.MaxValue)]
    public void TestSuccess(int id)
    {
        var componentId = new ComponentId(id);
        Assert.AreEqual(id, componentId.Value);
    }
}
