using Trees;

namespace Tests;

[TestClass]
public class TreesTests
{
    [TestMethod]
    public void TreeToArray_Success()
    {
        int[] input = [7, 1, 10, 4, 6, 9, 2, 11, 3, 5, 12, 8];
        var tree = new BinaryTree<int>();
        tree.AddRange(input);
        var result = tree.ToArray();
        Assert.AreEqual(input.Length, result.Length);
        for (int i = 0; i < input.Length; i++)
            Assert.AreEqual(input[i], result[i]);
    }

    [TestMethod]
    public void MakeHeap_Success()
    {
        int[] input = [7, 1, 10, 4, 6, 9, 2, 11, 3, 5, 12, 8];
        var heap = new Heap<int>();
        heap.MakeHeap(input);

        for (int i = input.Length - 1; i >= 0; i--)
        {
            var parentIndex = (i - 1) / 2;
            Assert.IsTrue(input[i] <= input[parentIndex]);
        }
    }

    [TestMethod]
    public void HeapSort_Success()
    {
        int[] input = [7, 1, 10, 4, 6, 9, 2, 11, 3, 5, 12, 8];
        var expected = input.Order().ToList();
        var heap = new Heap<int>();
        heap.MakeHeap(input);
        heap.HeapSort();
        CollectionAssert.AreEqual(expected, input);
    }
}