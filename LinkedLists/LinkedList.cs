using System.Collections;

namespace LinkedLists
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private readonly LinkedListNode<T> _topSentinel = new();
        private readonly LinkedListNode<T> _bottomSentinel = new();

        public LinkedListNode<T>? First => _topSentinel.Next;
        public LinkedListNode<T>? Last => _bottomSentinel.Previous;

        public void AddLast(T value)
        {
            var newNode = new LinkedListNode<T>(value);
            if (IsEmpty)
            {
                AddNodeForEmptyList(newNode);
            }
            else
            {
                PlaceAsLast(newNode);
            }    
        }

        public void AddFirst(T value)
        {
            var newNode = new LinkedListNode<T>(value);
            if (IsEmpty)
                AddNodeForEmptyList(newNode);
            else
                PlaceAsFirst(newNode);
        }

        public LinkedListNode<T>? FindNodeByValue(T value, RearrangeKind? rearrangeKind)
        {
            var node = Find(value);
            if (node is not null && rearrangeKind is not null)
                Rearrange(node, rearrangeKind);
            return node;
        }
        
        private LinkedListNode<T>? Find(T value)
        {
            var currentNode = _topSentinel;
            int i = 0;
            while (currentNode != _bottomSentinel)
            {
                if (currentNode is null || currentNode.Value is null)
                    continue;
                
                currentNode = currentNode.Next;
                if (EqualityComparer<T>.Default.Equals(currentNode.Value, value))
                    return currentNode;
            }
            return null;
        }

        private void Rearrange(LinkedListNode<T> node, RearrangeKind? rearrangeKind)
        {
            switch (rearrangeKind)
            {
                case RearrangeKind.MoveToFront:
                    MoveToFront(node);
                    break;
                case RearrangeKind.Swap:
                    SwapLeft(node);
                    break;
                case RearrangeKind.Count:
                    break;
                default:
                    return;
            }
        }

        public void MoveToFront(LinkedListNode<T> node)
        {
            RemoveNode(node);
            PlaceAsFirst(node);
        }

        public void SwapLeft(LinkedListNode<T> node)
        {
            var swapNode = node.Previous;
            if (swapNode == _topSentinel)
                return;

            var leftNode = swapNode!.Previous;
            var rightNode = node.Next;
            node.Next = swapNode;
            node.Previous = leftNode;
            swapNode.Next = rightNode;
            swapNode.Previous = node;
            leftNode!.Next = node;
            rightNode!.Previous = swapNode;
        }

        private void PlaceAsFirst(LinkedListNode<T> node)
        {
            var firstNode = _topSentinel.Next;
            firstNode!.Previous = node;
            node.Next = firstNode;
            node.Previous = _topSentinel;
            _topSentinel.Next = node;
        }
        
        private void PlaceAsLast(LinkedListNode<T> newNode)
        {
            var lastNode = _bottomSentinel.Previous;
            lastNode!.Next = newNode;
            newNode.Previous = lastNode;
            newNode.Next = _bottomSentinel;
            _bottomSentinel.Previous = newNode;
        }

        private void RemoveNode(LinkedListNode<T> node)
        {
            var previous = node.Previous;
            var next = node.Next;
            previous!.Next = next;
            next!.Previous = previous;
        }
        
        private void AddNodeForEmptyList(LinkedListNode<T> node)
        {
            _topSentinel.Next = node;
            _bottomSentinel.Previous = node;
            node.Next = _bottomSentinel;
            node.Previous = _topSentinel;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(_topSentinel);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsEmpty => _topSentinel.Next is null && _bottomSentinel.Previous is null;

        public void AddRange(IEnumerable<T> nodeValues)
        {
            foreach (var value in nodeValues)
                AddLast(value);
        }
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
}
