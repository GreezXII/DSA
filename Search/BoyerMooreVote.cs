namespace Search;

public static class BoyerMooreVote
{
    public static T? Search<T>(T[] data, IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;

        if (data.Length < 1)
            return default;

        var result = default(T);
        var counter = 0;
        foreach (var item in data)
        {
            if (counter == 0)
                result = item;

            if (comparer.Compare(item, result) == 0)
                counter++;
            else
                counter--;
        }

        return result;
    }
}