using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using LinkedLists;

BenchmarkRunner.Run<LinkedListsSearchBenchmark>();

public class LinkedListsSearchBenchmark
{
    private readonly LinkedLists.LinkedList<int> _linkedList = new();
    private readonly int[] _valuesToFind;

    public LinkedListsSearchBenchmark()
    {
        const int seed = 0;
        var random = new Random(seed);
        var nodeValues = Enumerable.Range(0, 1_000_000).ToArray();
        random.Shuffle(nodeValues);
        
        var temp = new List<int>();
        temp.AddRange(Enumerable.Repeat(0, 25));
        temp.AddRange(Enumerable.Repeat(1, 10));
        temp.AddRange(Enumerable.Repeat(2, 5));
        temp.AddRange(Enumerable.Repeat(3, 4));
        temp.AddRange(Enumerable.Repeat(4, 3));
        temp.AddRange(Enumerable.Repeat(5, 3));
        temp.AddRange(Enumerable.Range(6, 50));
        _valuesToFind = temp.ToArray();
        random.Shuffle(_valuesToFind);
        
        _linkedList.AddRange(nodeValues);
    }
    
    private void Search(RearrangeKind? rearrangeKind)
    {
        foreach (var i in _valuesToFind)
        {
            var node = _linkedList.FindNodeByValue(i, rearrangeKind);
            if (node?.Value != i)
                throw new Exception();
        }
    }

    [Benchmark]
    public void NoRearrange()
    {
        Search(null);
    }

    [Benchmark]
    public void MoveToFront()
    {
        Search(RearrangeKind.MoveToFront);
    }
    
    [Benchmark]
    public void Swap()
    {
        Search(RearrangeKind.Swap);
    }
}