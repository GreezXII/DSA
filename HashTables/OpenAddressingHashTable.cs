using System.Text;

namespace HashTables;

public class OpenAddressingHashTable<TKey, TValue>
{
    private readonly Entry<TKey, TValue>?[] _entries;
    private readonly EqualityComparer<TKey> _keyComparer;

    public ProbeSequenceStatistic Statistic { get; }
    
    public int Size { get; }
    public int Count { get; private set; }

    public OpenAddressingHashTable(int size)
    {
        Size = size;
        _entries = new Entry<TKey, TValue>?[Size];
        _keyComparer = EqualityComparer<TKey>.Default;
        Statistic = new ProbeSequenceStatistic();
    }

    public bool TryAdd(TKey key, TValue value)
    {
        int counter = 0;
        while (counter < Size)
        {
            var hash = GetHash(key, counter);
            var isCollision = _entries[hash] is null;
            if (isCollision)
            {
                _entries[hash] = new Entry<TKey, TValue>(key, value);
                Count++;
                Statistic.AddProbe(counter);
                return true;
            }
            counter++;
        }

        return false;
    }

    public bool TryGet(TKey key, out TValue? value)
    {
        int counter = 0;
        while (counter < Size)
        {
            var hash = GetHash(key, counter);
            var item = _entries[hash];
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
        return (Math.Abs(key.GetHashCode()) + counter) % Size;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var entry in _entries)
        {
            if (entry is null || entry.Value is null)
            {
                sb.Append("---");
            }
            else
            {
                sb.Append(entry.Value.ToString()?.PadLeft(3, ' '));
            }
            sb.Append("   ");
        }
        return sb.ToString();
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