using System.Collections;

namespace LinkedLists
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private readonly LinkedListNode<T> _topSentinel = new();
        private readonly LinkedListNode<T> _lastSentinel = new();

        public LinkedListNode<T>? First => _topSentinel.Next;
        public LinkedListNode<T>? Last => _lastSentinel.Previous;

        public void AddLast(T value)
        {
            var newNode = new LinkedListNode<T>(value);
            if (IsEmpty)
            {
                AddNodeForEmptyList(newNode);
            }
            else
            {
                var lastNode = _lastSentinel.Previous;
                lastNode!.Next = newNode;
                newNode.Previous = lastNode;
                _lastSentinel.Previous = newNode;
            }    
        }

        public void AddFirst(T value)
        {
            var newNode = new LinkedListNode<T>(value);
            if (IsEmpty)
            {
                AddNodeForEmptyList(newNode);
            }
            else
            {
                var firstNode = _topSentinel.Next;
                firstNode!.Previous = newNode;
                newNode.Next = firstNode;
                _topSentinel.Next = newNode;
            }
        }

        private void AddNodeForEmptyList(LinkedListNode<T> node)
        {
            _topSentinel.Next = node;
            _lastSentinel.Previous = node;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(_topSentinel);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsEmpty => _topSentinel.Next is null && _lastSentinel.Previous is null;
    }

    public class LinkedListNode<T>
    {
        public LinkedListNode<T>? Previous { get; set; }
        public LinkedListNode<T>? Next;
        public T? Value { get; set; }

        public LinkedListNode() { }

        public LinkedListNode(T value)
        {
            Value = value;
        }
    }

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
            if (_currentNode.Next is null)
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
}
