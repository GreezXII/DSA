namespace Sorting;

public class MergeSort
{
    public static void Sort(int[] data, int[] scratch, int start, int end)
    {
        if (start == end)
            return;

        var middle = (start + end) / 2;
        Sort(data, scratch, start, middle);
        Sort(data, scratch, middle + 1, end);
        
        int left = start;
        int right = middle + 1;
        int scratchIndex = start;
        while (left <= middle && right <= end)
        {
            if (data[left] <= data[right])
            {
                scratch[scratchIndex] = data[left];
                left++;
            }
            else
            {
                scratch[scratchIndex] = data[right];
                right++;
            }
            scratchIndex++;
        }

        for (int i = left; i <= middle; i++)
        {
            scratch[scratchIndex] = data[i];
            scratchIndex++;
        }

        for (int i = right; i <= end; i++)
        {
            scratch[scratchIndex] = data[i];
            scratchIndex++;
        }

        for (int i = start; i <= end; i++)
        {
            data[i] = scratch[i];
        }
    }
}