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

    private Entry<TKey, TValue> GetEntry(TKey key)
    {
        var hash = GetHash(key);
        var entry = _buckets[hash];
        if (entry is null)
            throw new KeyNotFoundException();
            
        var comparer = EqualityComparer<TKey>.Default;
        while (entry is not null)
        {
            if (comparer.Equals(entry.Key, key))
                return entry;
            entry = entry.Next;
        }
        throw new KeyNotFoundException();
    }
    
    public TValue? this[TKey key]
    {
        get => GetEntry(key).Value;
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            try
            {
                GetEntry(key).Value = value;
            }
            catch (KeyNotFoundException)
            {
                Add(key, value);
            }
        }
    }

    private int GetHash(TKey key)
    {
        if (key is null)
            throw new ArgumentNullException(nameof(key));
        return Math.Abs(key.GetHashCode()) % _bucketsCount;
    }
}

internal class Entry<TKey, TValue>
{
    public Entry(TKey key, TValue value, Entry<TKey, TValue>? next = null)
    {
        Key = key;
        Value = value;
        Next = next;
    }

    public TKey Key { get; private set; }
    public TValue Value { get; set; }
    public Entry<TKey, TValue>? Next { get; private set; }
}