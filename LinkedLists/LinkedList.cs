using System.Collections;

namespace LinkedLists
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private LinkedListNode<T> _top_sentinel = new();
        private LinkedListNode<T> _last_sentinel = new();

        public LinkedListNode<T>? First => _top_sentinel.Next;
        public LinkedListNode<T>? Last => _last_sentinel.Previous;

        public void Add(T value)
        {
            var newNode = new LinkedListNode<T>(value);
            if (IsEmpty)
            {
                _top_sentinel.Next = newNode;
                _last_sentinel.Previous = newNode;
            }
            else
            {
                var lastNode = _last_sentinel.Previous;
                lastNode!.Next = newNode;
                newNode.Previous = lastNode;
                _last_sentinel.Previous = newNode;
            }    
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(First!);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsEmpty { get => _top_sentinel.Next is null && _last_sentinel.Previous is null; }
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

    class LinkedListEnumerator<T> : IEnumerator<T>
    {
        private LinkedListNode<T> _currentNode;

        public T Current
        {
            get
            {
                var value = _currentNode.Value;
                _currentNode = _currentNode.Next!;
                return value!;
            }
        }

        object IEnumerator.Current => Current!;

        public LinkedListEnumerator(LinkedListNode<T> currentNode)
        {
            _currentNode = currentNode;
        }

        public bool MoveNext() => _currentNode is not null;

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }
    }
}
