using Trees;

namespace Tests;

[TestClass]
public class TreesTests
{
    [TestMethod]
    public void TreeToArray_Success()
    {
        var input = new int[] { 7, 1, 10, 4, 6, 9, 2, 11, 3, 5, 12, 8 };
        var tree = new BinaryTree<int>();
        tree.AddRange(input);
        var result = tree.ToArray();
        Assert.AreEqual(input.Length, result.Length);
        for (int i = 0; i < input.Length; i++)
            Assert.AreEqual(input[i], result[i]);
    }
}