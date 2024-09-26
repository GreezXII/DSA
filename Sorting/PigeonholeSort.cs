namespace Sorting;

public class PigeonholeSort
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
        var length = max - min + 1;  // +1 is for zero
        var holes = new Cell?[length];

        foreach (var value in array)
        {
            var holeIndex = value - min;
            var cell = new Cell(value);
            cell.Next = holes[holeIndex];
            holes[holeIndex] = cell;
        }

        var index = 0;
        for (var i = 0; i < array.Length; i++)
        {
            var cell = holes[i];
            if (cell is not null)
            {
                array[index] = cell.Value;
                index++;
            }
            while (cell?.Next is not null)
            {
                array[index] = cell.Value;
                index++;
            }
        }
    }
}

class Cell
{
    public Cell(int value)
    {
        Value = value;
    }

    public int Value { get; }
    public Cell? Next { get; set; }
}