namespace Sorting;

public class SelectionSort
{
    public static IList<T> Sort<T>(IList<T> data, IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;

        for (int i = 0; i < data.Count - 1; i++)
        {
            var minIndex = i;
            for (int j = i + 1; j < data.Count; j++)
            {
                if (comparer.Compare(data[minIndex], data[j]) > 0)
                {
                    minIndex = j;
                }
            }
            (data[i], data[minIndex]) = (data[minIndex], data[i]);
        }

        return data;
    }
}