namespace Sorting;

public class QuicksortAlgorithm
{
    public static void Quicksort(int[] values, int start, int end)
    {
        if (start >= end)
            return;

        var divider = values[start];
        var left = start;
        var right = end;

        while (true)
        {
            while (values[right] >= divider)
            {
                right--;
                if (right <= left)
                    break;
            }

            if (right <= left)
            {
                values[left] = divider;
                break;
            }
            
            values[left] = values[right];
            
            left++;
            while (values[left] < divider)
            {
                left++;
                if (left >= right)
                    break;
            }

            if (left >= right)
            {
                values[right] = divider;
                break;
            }
            
            values[right] = values[left];
        }
        
        Quicksort(values, start, right - 1);
        Quicksort(values, right + 1, end);
    }

    private static void Swap(int[] values, int a, int b) => (values[a], values[b]) = (values[b], values[a]);
}

public static class DividerSelector
{
    public static int MedianOfThree(int[] values, int start, int end)
    {
        if (values.Length == 1)
            return 0;
        
        var a = values[start];
        var b = values[end / 2];
        var c = values[end];

        // b a c  c a b
        if ((a > b && a < c) || (a > c && a < b))
            return a;
        // a b c  c b a
        if ((b > a && b < c) || (b > c && b < a))
            return b;

        return c;
    }
}
