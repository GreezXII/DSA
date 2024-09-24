namespace Sorting;

class Countingsort
{
    public static void Sort(int[] array)
    {
        if (array.Length < 1)
            return;
        
        var bounds = SortHelpers.FindBounds(array);
        if (bounds is null)
            return;

        var min = bounds.Value.Min;
        var max = bounds.Value.Max;
        var length = max - min + 1; // + 1 for zero
        var counter = new int[length];

        for (var i = 0; i < length; i++)
        {
            var index = array[i] - min;
            counter[index]++;
        }

        for (var i = 0; i < length; i++)
        {
            if (counter[i] == 0)
                continue;

            for (var j = 0; j < counter[i]; j++)
            {
                var value = i + min;
                array[i + j] = value;
            }
        }
    }
}
