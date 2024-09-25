using Sorting;

namespace Tests;

[TestClass]
public class SortTests
{
    private static TestContext _testContext;
    
    [ClassInitialize]
    public static void SetupTests(TestContext testContext)
    {
        _testContext = testContext;
    }
    
    [TestMethod]
    public void Quicksort_Success()
    {
        var q = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        foreach (var values in GeneratePermutations(q, 0, q.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            QuicksortAlgorithm.Quicksort(values, 0, values.Length - 1);
            for (int i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }

    [TestMethod]
    public void Mergesort_Success()
    {
        var q = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        foreach (var values in GeneratePermutations(q, 0, q.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            var scratch = new int[expected.Length];
            Array.Copy(expected, 0, scratch, 0, expected.Length);
            MergesortAlgorithm.Mergesort(values, scratch, 0, values.Length - 1);
            for (int i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }

    [TestMethod]
    public void Countingsort_Success()
    {
        var q = new int[] { -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        foreach (var values in GeneratePermutations(q, 0, q.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            Countingsort.Sort(values);
            for (var i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }

    [TestMethod]
    public void Pigeonholesort_Success()
    {
        var q = new int[] { -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        foreach (var values in GeneratePermutations(q, 0, q.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            Pigeonholesort.Sort(values);
            for (var i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }

    [TestMethod]
    public void Bucketsort_Success()
    {
        var q = new int[] { -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        foreach (var values in GeneratePermutations(q, 0, q.Length - 1))
        {
            var expected = values.OrderBy(x => x).ToArray();
            Bucketsort.Sort(values, 5);
            for (var i = 0; i < values.Length - 1; i++)
                Assert.AreEqual(expected[i], values[i]);
        }
    }
    
    static IEnumerable<int[]> GeneratePermutations(int[] arr, int start, int end)
    {
        if (start == end)
        {
            // Print the permutation
            _testContext.WriteLine(string.Join(", ", arr));
            yield return arr;
        }
        else
        {
            for (int i = start; i <= end; i++)
            {
                Swap(ref arr[start], ref arr[i]);
                GeneratePermutations(arr, start + 1, end);
                yield return arr;
                Swap(ref arr[start], ref arr[i]); // Backtrack
            }
        }
    }

    static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }
}