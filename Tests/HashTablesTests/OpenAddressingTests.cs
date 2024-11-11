using HashTables;

namespace Tests.HashTablesTests;

[TestClass]
public class OpenAddressingTests
{
    [TestMethod]
    public void LinearProbing_Add_Success()
    {
        var table = new OpenAddressingHashTable<int, string>(100, ProbingKind.Linear);
        for (int i = 0; i < 100; i++)
            table.TryAdd(i, i.ToString());

        for (int i = 0; i < 100; i++)
        {
            var result = table.TryGet(i, out var value);
            Assert.IsTrue(result);
            Assert.AreEqual(i.ToString(), value);
        }
    }

    [TestMethod]
    public void LinearProbing_Overflow()
    {
        var table = new OpenAddressingHashTable<int, string>(5, ProbingKind.Linear);
        for (int i = 0; i < 5; i++)
        {
            var result = table.TryAdd(i, i.ToString());
            Assert.IsTrue(result);
        }

        var overflowResult = table.TryAdd(5, "5");
        Assert.IsFalse(overflowResult);
    }

    [TestMethod]
    public void LinearProbing_Get_NotFound()
    {
        var table = new OpenAddressingHashTable<int, string>(100, ProbingKind.Linear);
        var result = table.TryGet(1, out var value);
        Assert.IsFalse(result);
        Assert.IsNull(value);
    }
}