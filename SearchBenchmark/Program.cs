using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<SearchBenchmark>();

public class SearchBenchmark
{
    private readonly int[] _data = Enumerable.Range(0, 1_000_000_000).ToArray();
    private const int Value = 3990000;

    [Benchmark]
    public void LinearSearch()
    {
        Search.LinearSearch.Search(_data, Value);
    }

    [Benchmark]
    public void BinarySearch()
    {
        Search.BinarySearch.Search(_data, Value);
    }

    [Benchmark]
    public void InterpolationSearch()
    {
        Search.InterpolationSearch.Search(_data, Value);
    }
}