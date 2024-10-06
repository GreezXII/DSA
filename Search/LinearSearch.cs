namespace Search;

public static class LinearSearch
{
    public static int Search<T>(T[] data, T value, IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;
        for (int i = 0; i < data.Length; i++)
        {
            if (comparer.Compare(data[i], value) == 0)
                return i;
        }

        return -1;
    }
}