namespace Search;

public static class InterpolationSearch
{
    public static int Search(int[] data, int value)
    {
        if (data.Length < 1)
            return -1;

        var min = 0;
        var max = data.Length - 1;

        while (min <= max)
        {
            var mid = min + (max - min) * (value - data[min]) / (data[max] - data[min]);
            
            if (mid < min || mid > max)
                mid = (max + min) / 2;
            
            if (data[mid] == value)
                return mid;

            if (data[mid] < value)
                min = mid + 1;
            else
                max = mid - 1;
        }

        return -1;
    }
}