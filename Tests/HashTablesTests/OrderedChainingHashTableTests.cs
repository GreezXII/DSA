using System.Reflection;
using System.Runtime.CompilerServices;
using HashTables;

namespace Tests;

[TestClass]
public class OrderedChainingHashTableTests
{
    [TestMethod]
    public void OrderedChainingHashTable_Add_Success()
    {
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(10);
        orderedChainingHashTable.Add("Fred", 42);
        orderedChainingHashTable.Add("Marta", 11);
        orderedChainingHashTable.Add("Greg", 55);
        orderedChainingHashTable.Add("Kevin", 15);

        Assert.AreEqual(42, orderedChainingHashTable["Fred"]);
        Assert.AreEqual(11, orderedChainingHashTable["Marta"]);
        Assert.AreEqual(55, orderedChainingHashTable["Greg"]);
        Assert.AreEqual(15, orderedChainingHashTable["Kevin"]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OrderedChainingHashTable_AddExistingKey_Exception()
    {
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(10);
        orderedChainingHashTable.Add("Fred", 42);
        orderedChainingHashTable.Add("Fred", 11);
    }

    [TestMethod]
    public void OrderedChainingHashTable_AddWithChaining_Success()
    {
        var orderedChainingHashTable = new OrderedChainingHashTable<SameHash, int>(10);
        orderedChainingHashTable.Add(new SameHash("A"), 42);
        orderedChainingHashTable.Add(new SameHash("B"), 11);
        orderedChainingHashTable.Add(new SameHash("C"), 55);
        orderedChainingHashTable.Add(new SameHash("D"), 15);

        Assert.AreEqual(42, orderedChainingHashTable[new SameHash("A")]);
        Assert.AreEqual(11, orderedChainingHashTable[new SameHash("B")]);
        Assert.AreEqual(55, orderedChainingHashTable[new SameHash("C")]);
        Assert.AreEqual(15, orderedChainingHashTable[new SameHash("D")]);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void OrderedChainingHashTable_ReadNonExistingKey_Exception()
    {
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(10);
        _ = orderedChainingHashTable["Fred"];
    }

    [TestMethod]
    public void OrderedChainingHashTable_Update_Success()
    {
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(10);
        orderedChainingHashTable.Add("Fred", 41);
        Assert.AreEqual(41, orderedChainingHashTable["Fred"]);
        orderedChainingHashTable["Fred"] = 11;
        Assert.AreEqual(11, orderedChainingHashTable["Fred"]);
    }

    [TestMethod]
    public void OrderedChainingHashTable_AddOrUpdate_Success()
    {
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(10);
        orderedChainingHashTable["Fred"] = 41;
        Assert.AreEqual(41, orderedChainingHashTable["Fred"]);
        orderedChainingHashTable["Fred"] = 11;
        Assert.AreEqual(11, orderedChainingHashTable["Fred"]);
    }

    [TestMethod]
    public void OrderedChainingHashTable_CheckSort_Success()
    {
        var listOfPairs = new List<(string, int)>
        {
            ("D", 4),
            ("B", 2),
            ("A", 1),
            ("C", 3),
            ("F", 6),
            ("G", 7),
            ("E", 5)
        };
        
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(1);
        foreach (var pair in listOfPairs)
            orderedChainingHashTable.Add(pair.Item1, pair.Item2);

        var type = typeof(OrderedChainingHashTable<string, int>);
        var field = type.GetField("_buckets", BindingFlags.Instance | BindingFlags.NonPublic);
        if (field!.GetValue(orderedChainingHashTable) is not LinkedEntry<string, int>?[] value)
            throw new InvalidOperationException();

        var orderedPairs = listOfPairs.OrderBy(pair => pair.Item1).ToList();
        var entry = value[0];
        for (int i = 0; i < orderedPairs.Count; i++)
        {
            Assert.AreEqual(orderedPairs[i].Item1, entry!.Key);
            Assert.AreEqual(orderedPairs[i].Item2, entry.Value);
            entry = entry.Next;
        }
    }
}