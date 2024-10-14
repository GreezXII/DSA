using System.Net.Http.Headers;

namespace HashTables;

public class ChainingHashTable<TKey, TValue>
{
    public ChainingHashTable(int bucketsCount, int length)
    {
        _bucketsCount = bucketsCount;
        _length = length;
        
        _buckets = new Entry<TKey, TValue>?[_bucketsCount];
    }

    private int _bucketsCount;
    private int _length;
    private readonly Entry<TKey, TValue>?[] _buckets;

    public void Add(TKey key, TValue value)
    {
        if (key is null)
            throw new ArgumentNullException(nameof(key));
        var hash = GetHash(key);
        var entry = _buckets[hash];
        if (entry is null)
        {
            entry = new Entry<TKey, TValue>(key, value);
            _buckets[hash] = entry;
            return;
        }

        var comparer = EqualityComparer<TKey>.Default;
        while (entry is not null)
        {
            if (comparer.Equals(entry.Key, key))
                throw new ArgumentException("Key already exists.");
            
            entry = entry.Next;
        }
        
        var newEntry = new Entry<TKey, TValue>(key, value, _buckets[hash]);
        _buckets[hash] = newEntry;
    }

    public TValue? this[TKey key]
    {
        get
        {
            var hash = GetHash(key);
            var entry = _buckets[hash];
            if (entry is null)
                return default;
            
            var comparer = EqualityComparer<TKey>.Default;
            while (entry is not null)
            {
                if (comparer.Equals(entry.Key, key))
                    return entry.Value;
                entry = entry.Next;
            }
            return default;
        }
    }

    private int GetHash(TKey key)
    {
        if (key is null)
            throw new ArgumentNullException(nameof(key));
        return Math.Abs(key.GetHashCode()) % _bucketsCount;
    }
}

public class Entry<TKey, TValue>
{
    public Entry(TKey key, TValue value, Entry<TKey, TValue>? next = null)
    {
        Key = key;
        Value = value;
        Next = next;
    }

    public TKey Key { get; private set; }
    public TValue Value { get; private set; }
    public Entry<TKey, TValue>? Next { get; private set; }
}