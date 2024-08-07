using Trees;

namespace Tests;

[TestClass]
public class TreesTests
{
    private int[] _input;
    [TestInitialize]
    public void TestInitialize()
    {
        _input = [7, 1, 10, 4, 6, 9, 2, 11, 3, 5, 12, 8];
    }
    
    [TestMethod]
    public void TreeToArray_Success()
    {
        var tree = new BinaryTree<int>();
        tree.AddRange(_input);
        var result = tree.ToArray();
        Assert.AreEqual(_input.Length, result.Length);
        for (int i = 0; i < _input.Length; i++)
            Assert.AreEqual(_input[i], result[i]);
    }

    [TestMethod]
    public void MakeHeap_Success()
    {
        var heap = new Heap<int>();
        heap.MakeHeap(_input);

        for (int i = _input.Length - 1; i >= 0; i--)
        {
            var parentIndex = (i - 1) / 2;
            Assert.IsTrue(_input[i] <= _input[parentIndex]);
        }
    }

    [TestMethod]
    public void PopTopItem_Success()
    {
        var expected = _input.OrderDescending().ToList();
        var heap = new Heap<int>();
        heap.MakeHeap(_input);

        for (int i = 0; i < _input.Length; i++)
        {
            var topItem = heap.PopTopItem();
            Assert.AreEqual(topItem, expected[i]);
        }
    }
}