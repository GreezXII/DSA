namespace Sorting;

public class Bucketsort
{
    public static void Sort(int[] array, int bucketsCounter)
    {
        if (bucketsCounter == 0)
            return;
        
        var buckets = new List<int>[bucketsCounter];
        for (var i = 0; i < buckets.Length; i++)
            buckets[i] = [];
        
        var bounds = SortHelpers.FindBounds(array);
        if (bounds is null)
            return;
        
        var bucketLength = (Math.Abs(bounds.Value.Min) + Math.Abs(bounds.Value.Max) + 1) / (double)buckets.Length;
        foreach (var value in array)
        {
            var bucketIndex = (int)((value + Math.Abs(bounds.Value.Min)) / bucketLength);
            var bucket = buckets[bucketIndex];
            bucket.Add(value);
        }

        foreach (var bucket in buckets)
            bucket.Sort();

        var index = 0;
        foreach (var bucket in buckets)
        {
            foreach (var value in bucket)
            {
                array[index] = value;
                index++;
            }
        }
    }
}
