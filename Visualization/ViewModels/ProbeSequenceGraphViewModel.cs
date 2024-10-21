using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using HashTables;

namespace Visualization.ViewModels;

public class ProbeSequenceGraphViewModel : ViewModelBase
{
    private const int BucketsCount = 10;
    public ObservableCollection<List<Point>> Data { get; set; } = new();

    public ProbeSequenceGraphViewModel()
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
        
        Data.Add(naiveData);
        Data.Add(orderedData);
    }
    
    private double GetProbeSequenceLengthForNaive(int bucketsCount, int itemsCount)
    {
        var chainingHashTable = new ChainingHashTable<string, int>(bucketsCount);
        foreach (var value in GetRandomNumbers(itemsCount))
            chainingHashTable.Add(value.ToString(), value);
        return chainingHashTable.ProbeSequence.Average;
    }

    private double GetProbeSequenceLengthForOrdered(int bucketsCount, int itemsCount)
    {
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(bucketsCount);
        foreach (var value in GetRandomNumbers(itemsCount))
            orderedChainingHashTable.Add(value.ToString(), value);
        return orderedChainingHashTable.ProbeSequence.Average;
    }

    private int[] GetRandomNumbers(int count)
    {
        var numbers = Enumerable.Range(0, count).ToArray();
        var random = new Random();
        random.Shuffle(numbers);
        return numbers;
    }
}