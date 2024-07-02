var numsInput = new int[] { 5, 2, 3, 6, 4, 1, 8, 0, 7, 9};
var charsInput = new char[] { 'a', 'c', 'b', 'e', 'f', 'd', 'g' };

var numResult = InsertionSort(Copy(numsInput));
var charResult = InsertionSort(Copy(charsInput));
Print(numResult);
Print(charResult);

numResult = SelectionSort(Copy(numsInput));
charResult = SelectionSort(Copy(charsInput));
Print(numResult);
Print(charResult);

numResult = BubbleSort(Copy(numsInput));
charResult = BubbleSort(Copy(charsInput));
Print(numResult);
Print(charResult);

Console.Read();

IList<T> InsertionSort<T>(IList<T> data, IComparer<T>? comparer = null)
{
    if (comparer is null)
        comparer = Comparer<T>.Default;
    for (int i = 1; i < data.Count; i++)
    {
        for (int j = 0; j < i; j++)
        {
            if (comparer.Compare(data[j], data[i]) > 0)
            {
                var temp = data[i];
                data[i] = data[j];
                data[j] = temp;
            }
        }
    }
    return data;
}

IList<T> SelectionSort<T>(IList<T> data, IComparer<T>? comparer = null)
{
    if (comparer is null)
        comparer = Comparer<T>.Default;

    for (int i = 0; i < data.Count; i++)
    {
        var minIndex = i;
        for (int j = i + 1; j < data.Count; j++)
        {
            if (comparer.Compare(data[minIndex], data[j]) > 0)
            {
                minIndex = j;
            }
        }
        var temp = data[i];
        data[i] = data[minIndex];
        data[minIndex] = temp;
    }

    return data;
}

IList<T> BubbleSort<T>(IList<T> data, IComparer<T>? comparer = null)
{
    if (comparer is null)
        comparer = Comparer<T>.Default;

    bool isSorted = false;
    while (!isSorted)
    {
        isSorted = true;
        for (int i = 1; i < data.Count; i++)
        {
            if (comparer.Compare(data[i - 1], data[i]) > 0)
            {
                var temp = data[i];
                data[i] = data[i - 1];
                data[i - 1] = temp;
                isSorted = false;
            }
        }
    }

    return data;
}


void Print<T>(IList<T> data)
{
    foreach (var item in data)
        Console.Write(item);

    Console.WriteLine();
}

IList<T> Copy<T>(IList<T> input)
{
    T[] output = Enumerable.Repeat<T>(default(T)!, input.Count).ToArray();
    input.CopyTo(output, 0);
    return output;
}
