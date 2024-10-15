namespace HashTables;

public class OrderedChainingHashTable<TKey, TValue>
{
    public OrderedChainingHashTable(int bucketsCount, int length)
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

        Entry<TKey, TValue>? previousEntry = null;
        var comparer = Comparer<TKey>.Default;
        while (entry is not null)
        {
            var compareResult = comparer.Compare(entry.Key, key); 
            if (compareResult == 0)
                throw new ArgumentException("Key already exists.");
            
            if (compareResult < 0)
            {
                previousEntry = entry;
                entry = entry.Next;
            }
            else
            {
                break;
            }
        }

        Entry<TKey, TValue> newEntry;
        if (previousEntry is not null)
        {
            newEntry = new Entry<TKey, TValue>(key, value, previousEntry.Next);
            previousEntry.Next = newEntry;
        }
        else
        {
            newEntry = new Entry<TKey, TValue>(key, value, _buckets[hash]);
            _buckets[hash] = newEntry;
        }
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