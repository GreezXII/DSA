namespace Trees;

class BinaryTree<T>
{
    private Node<T>? _root;

    private int _count = 0;
    
    public void AddNode(T value)
    {
        if (_root is null)
        {
            _root = new Node<T> { Value = value };
            _count++;
            return;
        }

        var currentNode = _root;
        while (true)
        {
            if (currentNode.Left is null)
            {
                currentNode.Left = new Node<T> { Value = value };
                _count++;
                return;
            }

            if (currentNode.Right is null)
            {
                currentNode.Right = new Node<T> { Value = value };
                _count++;
                return;
            }
            currentNode = currentNode.Left;
        }
    }

    public void AddRange(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            AddNode(value);
        }
    }

    public T[] ToArray()
    {
        var result = new T[_count];
        if (_root is null)
            return result;

        int index = 0;
        result[0] = _root.Value;
        if (_root.Left is null)
            return result;

        result[1] = _root.Left.Value;

        if (_root.Right is null)
            return result;

        result[2] = _root.Right.Value;

        var currentNode = _root.Left;
        for (int i = 1; i < _count; i++)
        {
            if (currentNode.Left is null)
                return result;
            
            int left = GetLeftIndex(i);
            result[left] = currentNode.Left.Value;

            if (currentNode.Right is null)
                return result;
            
            int right = GetRightIndex(i);
            result[right] = currentNode.Right.Value;

            currentNode = currentNode.Left;
        }

        return result;
    }

    private int GetLeftIndex(int i) => 2 * i + 1;

    private int GetRightIndex(int i) => 2 * i + 2;
}

class Node<T>
{
    public T? Value { get; set; }
    public Node<T>? Left { get; set; }
    public Node<T>? Right { get; set; }
}

class Heap<T>()
{
    private T[] _items;
    public IReadOnlyCollection<T> Items => _items;
    
    public void HeapSort(Comparer<T>? comparer = null)
    {
        if (_items.Length == 0)
            return;

        comparer ??= Comparer<T>.Default;

        int end = _items.Length - 1;
        while (end > 0)
        {
            (_items[0], _items[end]) = (_items[end], _items[0]);
            int index = 0;
            while (true)
            {
                int leftIndex = (2 * index) + 1;
                int rightIndex = (2 * index) + 2;

                if (leftIndex >= end)
                    leftIndex = index;
                if (rightIndex >= end)
                    rightIndex = index;

                var indexValue = _items[index];
                var leftValue = _items[leftIndex];
                var rightValue = _items[rightIndex];
                if (comparer.Compare(indexValue, leftValue) >= 0 && comparer.Compare(indexValue, rightValue) >= 0)
                    break;

                var swapIndex = comparer.Compare(leftValue, rightValue) > 0 ? leftIndex : rightIndex;

                (_items[index], _items[swapIndex]) = (_items[swapIndex], _items[index]);
                index = swapIndex;
            }
            end--;
        }
    }
    
    public void MakeHeap(T[] input, Comparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;
        for (int i = 0; i < input.Length; i++)
        {
            int index = i;
            while (index != 0)
            {
                int parentIndex = (index - 1) / 2;
                var compare = comparer.Compare(input[index], input[parentIndex]); 
                if (compare < 1)
                    break;

                (input[index], input[parentIndex]) = (input[parentIndex], input[index]);
                index = parentIndex;
            }
        }
        _items = input;
    }
}