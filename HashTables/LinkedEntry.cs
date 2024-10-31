namespace HashTables;

public class LinkedEntry<TKey, TValue>
{
    public LinkedEntry(TKey key, TValue value, LinkedEntry<TKey, TValue>? next = null)
    {
        Key = key;
        Value = value;
        Next = next;
    }

    public TKey Key { get; private set; }
    public TValue Value { get; set; }
    public LinkedEntry<TKey, TValue>? Next { get; set; }
}