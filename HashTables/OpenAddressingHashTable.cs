namespace HashTables;

public class OpenAddressingHashTable<TKey, TValue>
{
    private readonly int _size;
    private readonly Entry<TKey, TValue>?[] _keys;
    private readonly EqualityComparer<TKey> _keyComparer;

    public OpenAddressingHashTable(int size)
    {
        _size = size;
        _keys = new Entry<TKey, TValue>?[_size];
        _keyComparer = EqualityComparer<TKey>.Default;
    }

    public bool TryAdd(TKey key, TValue value)
    {
        int counter = 0;
        while (counter < _size)
        {
            var hash = GetHash(key, counter);
            var isCollision = _keys[hash] is null;
            if (isCollision)
            {
                _keys[hash] = new Entry<TKey, TValue>(key, value);;
                return true;
            }
            counter++;
        }

        return false;
    }

    public bool TryGet(TKey key, out TValue? value)
    {
        int counter = 0;
        while (counter < _size)
        {
            var hash = GetHash(key, counter);
            var item = _keys[hash];
            if (item is null)
            {
                value = default;
                return false;
            }

            if (_keyComparer.Equals(key, item.Key))
            {
                if (item.IsDeleted)
                {
                    value = default;
                    return false;
                }
                value = item.Value;
                return true;
            }
            counter++;
        }
        value = default;
        return false;
    }

    private int GetHash(TKey key, int counter)
    {
        if (key is null)
            throw new ArgumentNullException(nameof(key));
        return (Math.Abs(key.GetHashCode()) + counter) % _size;
    }
}

public class Entry<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; }
    public bool IsDeleted { get; private set; }

    public Entry(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    public void Delete() => IsDeleted = true;
}