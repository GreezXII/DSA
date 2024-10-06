namespace Search;

public static class BinarySearch
{
    public static int Search<T>(T[] data, T value, IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;

        if (data.Length < 1)
            return -1;

        var min = 0;
        var max = data.Length - 1;

        while (min <= max)
        {
            var mid = (min + max) / 2;
            
            if (comparer.Compare(data[mid], value) == 0)
                return mid;
            
            else if (comparer.Compare(data[mid], value) < 0)
                min = mid + 1;
            
            else
                max = mid - 1;
        }

        return -1;
    }
}