namespace Sorting;

public class BubbleSort
{
    public static IList<T> Sort<T>(IList<T> data, IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;

        bool isSorted = false;
        while (!isSorted)
        {
            isSorted = true;
            for (int i = 1; i < data.Count; i++)
            {
                if (comparer.Compare(data[i - 1], data[i]) > 0)
                {
                    (data[i], data[i - 1]) = (data[i - 1], data[i]);
                    isSorted = false;
                }
            }
        }
        return data;
    }
}