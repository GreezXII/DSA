namespace Sorting;

public class InsertionSort
{
    public static void Sort<T>(T[] data, IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;
        for (int i = 1; i < data.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (comparer.Compare(data[j], data[i]) > 0)
                {
                    (data[i], data[j]) = (data[j], data[i]);
                }
            }
        }
    }
}