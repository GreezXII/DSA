using System.Diagnostics;
using HashTables;

namespace Tests;

[TestClass]
public class ChainingHashTablesTests
{
    [TestMethod]
    public void ChainingHashTable_Add_Success()
    {
        var chainingHashTable = new ChainingHashTable<string, int>(10, 100);
        chainingHashTable.Add("Fred", 42);
        chainingHashTable.Add("Marta", 11);
        chainingHashTable.Add("Greg", 55);
        chainingHashTable.Add("Kevin", 15);

        Assert.AreEqual(42, chainingHashTable["Fred"]);
        Assert.AreEqual(11, chainingHashTable["Marta"]);
        Assert.AreEqual(55, chainingHashTable["Greg"]);
        Assert.AreEqual(15, chainingHashTable["Kevin"]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ChainingHashTable_AddExistingKey_Exception()
    {
        var chainingHashTable = new ChainingHashTable<string, int>(10, 100);
        chainingHashTable.Add("Fred", 42);
        chainingHashTable.Add("Fred", 11);
    }

    [TestMethod]
    public void ChainingHashTable_AddWithChaining_Success()
    {
        var chainingHashTable = new ChainingHashTable<SameHash, int>(10, 100);
        chainingHashTable.Add(new SameHash("A"), 42);
        chainingHashTable.Add(new SameHash("B"), 11);
        chainingHashTable.Add(new SameHash("C"), 55);
        chainingHashTable.Add(new SameHash("D"), 15);

        Assert.AreEqual(42, chainingHashTable[new SameHash("A")]);
        Assert.AreEqual(11, chainingHashTable[new SameHash("B")]);
        Assert.AreEqual(55, chainingHashTable[new SameHash("C")]);
        Assert.AreEqual(15, chainingHashTable[new SameHash("D")]);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void ChainingHashTable_ReadNonExistingKey_Exception()
    {
        var chainingHashTable = new ChainingHashTable<string, int>(10, 100);
        _ = chainingHashTable["Fred"];
    }

    [TestMethod]
    public void ChainingHashTable_Update_Success()
    {
        var chainingHashTable = new ChainingHashTable<string, int>(10, 100);
        chainingHashTable.Add("Fred", 41);
        Assert.AreEqual(41, chainingHashTable["Fred"]);
        chainingHashTable["Fred"] = 11;
        Assert.AreEqual(11, chainingHashTable["Fred"]);
    }
    
    [TestMethod]
    public void ChainingHashTable_AddOrUpdate_Success()
    {
        var chainingHashTable = new ChainingHashTable<string, int>(10, 100);
        chainingHashTable["Fred"] = 41;
        Assert.AreEqual(41, chainingHashTable["Fred"]);
        chainingHashTable["Fred"] = 11;
        Assert.AreEqual(11, chainingHashTable["Fred"]);
    }
}
