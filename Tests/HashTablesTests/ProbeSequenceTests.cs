using HashTables;

namespace Tests;

[TestClass]
public class ProbeSequenceTests
{
    [TestMethod]
    public void ProbeSequence_OrderedVsUnordered()
    {
        var chainingHashTable = new ChainingHashTable<string, int>(10);
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(10);

        foreach (int i in Enumerable.Range(0, 100))
        {
            chainingHashTable.Add(i.ToString(), i);
            orderedChainingHashTable.Add(i.ToString(), i);
        }

        var naiveAverage = chainingHashTable.ProbeSequence.Average;
        var orderedAverage = orderedChainingHashTable.ProbeSequence.Average;
        if (!naiveAverage.HasValue)
            throw new NullReferenceException(nameof(naiveAverage));
        if (!orderedAverage.HasValue)
            throw new NullReferenceException(nameof(orderedAverage));
        var delta = Math.Abs(naiveAverage.Value - orderedAverage.Value);
        Assert.IsTrue(delta < 1.0);
    }

    [TestMethod]
    public void ProbeSequence_OrderedVsUnordered_Reversed()
    {
        var chainingHashTable = new ChainingHashTable<string, int>(10);
        var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(10);

        foreach (int i in Enumerable.Range(0, 100).Reverse())
        {
            chainingHashTable.Add(i.ToString(), i);
            orderedChainingHashTable.Add(i.ToString(), i);
        }

        Assert.IsTrue(orderedChainingHashTable.ProbeSequence.Average < chainingHashTable.ProbeSequence.Average);
    }

    [TestMethod]
    public void ProbeSequence_OrderedVsUnordered_Random()
    {
        const int totalRepeats = 100;
        double naiveAverage = 0;
        double orderedAverage = 0;
        for (var i = 0; i < totalRepeats; i++)
        {
            var chainingHashTable = new ChainingHashTable<string, int>(10);
            var orderedChainingHashTable = new OrderedChainingHashTable<string, int>(10);

            var numbers = Enumerable.Range(0, 100).ToArray();
            var random = new Random();
            random.Shuffle(numbers);
            foreach (var value in numbers)
            {
                chainingHashTable.Add(value.ToString(), value);
                orderedChainingHashTable.Add(value.ToString(), value);
            }

            var newNaiveAverage = chainingHashTable.ProbeSequence.Average;
            if (!newNaiveAverage.HasValue)
                throw new NullReferenceException(nameof(newNaiveAverage));
            
            var newOrderedAverage = orderedChainingHashTable.ProbeSequence.Average;
            if (!newOrderedAverage.HasValue)
                throw new NullReferenceException(nameof(newOrderedAverage));
            
            naiveAverage += newNaiveAverage.Value;
            orderedAverage += newOrderedAverage.Value;
        }

        Assert.IsTrue((naiveAverage / totalRepeats) > (orderedAverage / totalRepeats));
    }
}