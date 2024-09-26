using Sorting;

namespace Tests;

[TestClass]
public class SortTests
{
    private static TestContext? _testContext;
    private static int[] _testIntegers = null!;

    [ClassInitialize]
    public static void SetupTests(TestContext testContext)
    {
        _testContext = testContext;
        _testIntegers = [-9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    }

    [TestMethod]
    public void InsertionSort_Success()
    {
        var testData = new int[_testIntegers.Length];
        Array.Copy(_testIntegers, testData, testData.Length);
        foreach (var values in GeneratePermutations(testData, 0, testData.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            InsertionSort.Sort(values);
            for (var i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }
    
    [TestMethod]
    public void SelectionSort_Success()
    {
        var testData = new int[_testIntegers.Length];
        Array.Copy(_testIntegers, testData, testData.Length);
        foreach (var values in GeneratePermutations(testData, 0, testData.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            SelectionSort.Sort(values);
            for (var i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }
    
    [TestMethod]
    public void BubbleSort_Success()
    {
        var testData = new int[_testIntegers.Length];
        Array.Copy(_testIntegers, testData, testData.Length);
        foreach (var values in GeneratePermutations(testData, 0, testData.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            BubbleSort.Sort(values);
            for (var i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }
    
    [TestMethod]
    public void QuickSort_Success()
    {
        var testData = new int[_testIntegers.Length];
        Array.Copy(_testIntegers, testData, testData.Length);
        foreach (var values in GeneratePermutations(testData, 0, testData.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            QuickSort.Sort(values, 0, values.Length - 1);
            for (int i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }

    [TestMethod]
    public void MergeSort_Success()
    {
        var testData = new int[_testIntegers.Length];
        Array.Copy(_testIntegers, testData, testData.Length);
        foreach (var values in GeneratePermutations(testData, 0, testData.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            var scratch = new int[expected.Length];
            Array.Copy(expected, 0, scratch, 0, expected.Length);
            MergeSort.Sort(values, scratch, 0, values.Length - 1);
            for (int i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }

    [TestMethod]
    public void CountingSort_Success()
    {
        var testData = new int[_testIntegers.Length];
        Array.Copy(_testIntegers, testData, testData.Length);
        foreach (var values in GeneratePermutations(testData, 0, testData.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            CountingSort.Sort(values);
            for (var i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }

    [TestMethod]
    public void PigeonholeSort_Success()
    {
        var testData = new int[_testIntegers.Length];
        Array.Copy(_testIntegers, testData, testData.Length);
        foreach (var values in GeneratePermutations(testData, 0, testData.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            PigeonholeSort.Sort(values);
            for (var i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }

    [TestMethod]
    public void BucketSort_Success()
    {
        var testData = new int[_testIntegers.Length];
        Array.Copy(_testIntegers, testData, testData.Length);
        foreach (var values in GeneratePermutations(testData, 0, testData.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            BucketSort.Sort(values, 5);
            for (var i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }

    private static IEnumerable<int[]> GeneratePermutations(int[] arr, int start, int end)
    {
        if (start == end)
        {
            yield return arr;
        }
        else
        {
            for (int i = start; i <= end; i++)
            {
                Swap(ref arr[start], ref arr[i]);
                _ = GeneratePermutations(arr, start + 1, end);
                yield return arr;
                Swap(ref arr[start], ref arr[i]); 
            }
        }
    }

    private static void Swap(ref int a, ref int b) => (a, b) = (b, a);
}