namespace Sorting;

class Countingsort
{
    public static void Sort(int[] array)
    {
        if (array.Length < 1)
            return;
        
        var bounds = FindBounds(array);
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

    private static Bounds? FindBounds(int[] array)
    {
        if (array.Length < 1)
            return null;
        
        var bounds = new Bounds(array[0], array[0]);
        for (var i = 1; i < array.Length; i++)
        {   
            if (array[i] > bounds.Max)
                bounds.Max = array[i];
            if (array[i] < bounds.Min)
                bounds.Min = array[i];
        }
        return bounds;
    }
}

internal struct Bounds(int min, int max)
{
    public int Min { get; set; } = min;
    public int Max { get; set; } = max;
}