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