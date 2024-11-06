using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using HashTables;
using Visualization.UserControls;

namespace Visualization.ViewModels;

public class ChainingViewModel : ViewModelBase
{
    public override string Title => "Chaining";

    private const int BucketsCount = 10;
    public ObservableCollection<Graph> Data { get; set; } = new();

    public ChainingViewModel()
    {
        CreateChartData();
    }

    private void CreateChartData()
    {
        var naiveData = new List<Point>();
        var orderedData = new List<Point>();

        for (int itemsCount = 50; itemsCount < 300; itemsCount += 50)
        {
            double naiveProbeSequence = GetProbeSequenceLengthForNaive(BucketsCount, itemsCount);
            var naivePoint = new Point(itemsCount, naiveProbeSequence);
            naiveData.Add(naivePoint);        
            
            double orderedProbeSequence = GetProbeSequenceLengthForOrdered(BucketsCount, itemsCount);
            var orderedPoint = new Point(itemsCount, orderedProbeSequence);
            orderedData.Add(orderedPoint);        
        }
        
        var naiveGraph = new Graph("Naive", naiveData);
        Data.Add(naiveGraph);
        var orderedGraph = new Graph("Ordered", orderedData);
        Data.Add(orderedGraph);
    }
    
    private double GetProbeSequenceLengthForNaive(int bucketsCount, int itemsCount)
    {
        var chainingHashTable = new ChainingHashTable<string, int>(bucketsCount);
        foreach (var value in GetRandomNumbers(itemsCount))
            chainingHashTable.Add(value.ToString(), value);
        var average = chainingHashTable.ProbeSequence.Average;
        if (average is null)
            throw new NullReferenceException(nameof(average));
        return average.Value;
    }

    private double GetProbeSequenceLengthForOrdered(int bucketsCount, int itemsCount)
    {
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(bucketsCount);
        foreach (var value in GetRandomNumbers(itemsCount))
            orderedChainingHashTable.Add(value.ToString(), value);
        var average = orderedChainingHashTable.ProbeSequence.Average;
        if (average is null)
            throw new NullReferenceException(nameof(average));
        return average.Value;
    }

    private int[] GetRandomNumbers(int count)
    {
        var numbers = Enumerable.Range(0, count).ToArray();
        var random = new Random();
        random.Shuffle(numbers);
        return numbers;
    }
}