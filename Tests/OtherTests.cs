using Other;

namespace Tests;

[TestClass]
public class OtherTests
{
    [TestMethod]
    public void HashCodesTest_Success()
    {
        var a0 = new A(0, "0", 0.0);
        var a1 = new A(1, "1", 1.0);
        var dict = new Dictionary<A, string>()
        {
            { a0, "0" },
            { a1, "1" }
        };
        
        Assert.AreEqual("0", dict[a0]);
        Assert.AreEqual("1", dict[a1]);
    }

    [TestMethod]
    public void HashCodesTest_Fail()
    {
        var b0 = new B(0, "0", 0.0);
        var b1 = new B(1, "1", 1.0);
        var dict = new Dictionary<B, string>()
        {
            { b0, "0" },
            { b1, "1" }
        };

        dict.TryGetValue(b0, out var v1);
        dict.TryGetValue(b1, out var v2);
        Assert.AreEqual(null, v1);
        Assert.AreEqual(null, v2);
    }
}