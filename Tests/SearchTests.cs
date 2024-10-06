using Search;

namespace Tests;

[TestClass]
public class SearchTests
{
    [TestMethod]
    public void LinearSearch_Success()
    {
        var input = Enumerable.Range(0, 1_000_000).ToArray();
        var value = 399000;
        
        var result = LinearSearch.Search(input, value);

        Assert.AreEqual(value, result);
    }

    [TestMethod]
    public void BinarySearch_Success()
    {
        var input = Enumerable.Range(0, 1_000_000).ToArray();
        var value = 399000;
        
        var result = BinarySearch.Search(input, value);

        Assert.AreEqual(value, result);
    }
    
    [TestMethod]
    public void InterpolationSearch_Success()
    {
        var input = Enumerable.Range(0, 1_000_000).ToArray();
        var value = 399000;
        
        var result = InterpolationSearch.Search(input, value);

        Assert.AreEqual(value, result);
    }
    
    [TestMethod]
    public void BoyerMooreVote_Success()
    {
        var input = new[] { 1, 1, 1, 1, 2, 2, 3, 3 };
        
        var result = BoyerMooreVote.Search(input);

        Assert.AreEqual(1, result);
    }
}