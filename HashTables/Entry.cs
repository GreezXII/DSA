namespace HashTables;

public class Entry<TKey, TValue>
{
    public Entry(TKey key, TValue value, Entry<TKey, TValue>? next = null)
    {
        Key = key;
        Value = value;
        Next = next;
    }

    public TKey Key { get; private set; }
    public TValue Value { get; set; }
    public Entry<TKey, TValue>? Next { get; set; }
}