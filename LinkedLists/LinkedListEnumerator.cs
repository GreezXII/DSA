using System.Collections;

namespace LinkedLists;

internal class LinkedListEnumerator<T> : IEnumerator<T>
{
    private LinkedListNode<T> _currentNode;

    public T Current => _currentNode.Value!;

    object IEnumerator.Current => Current!;

    public LinkedListEnumerator(LinkedListNode<T> currentNode)
    {
        _currentNode = currentNode;
    }

    public bool MoveNext()
    {
        if (_currentNode.Next?.Next is null)
            return false;

        _currentNode = _currentNode.Next!;
        return true;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Dispose() { }
}