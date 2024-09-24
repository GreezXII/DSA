namespace Sorting;

internal static class SortHelpers
{
    public static Bounds? FindBounds(int[] array)
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