﻿namespace HashTables;

public class OrderedChainingHashTable<TKey, TValue>
{
    
    private readonly int _bucketsCount;
    private readonly LinkedEntry<TKey, TValue>?[] _buckets;

    public ProbeSequenceStatistic ProbeSequence { get; }
    
    public OrderedChainingHashTable(int bucketsCount)
    {
        _bucketsCount = bucketsCount;
        _buckets = new LinkedEntry<TKey, TValue>?[_bucketsCount];
        ProbeSequence = new ProbeSequenceStatistic();
    }

    public void Add(TKey key, TValue value)
    {
        if (key is null) 
            throw new ArgumentNullException(nameof(key));

        var hash = GetHash(key);
        var entry = _buckets[hash];
        if (entry is null)
        {
            entry = new LinkedEntry<TKey, TValue>(key, value);
            _buckets[hash] = entry;
            ProbeSequence.AddProbe(0);
            return;
        }

        int probeCount = 0;
        LinkedEntry<TKey, TValue>? previousEntry = null;
        var comparer = Comparer<TKey>.Default;
        while (entry is not null)
        {
            probeCount++;
            ProbeSequence.AddProbe(probeCount);
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

        LinkedEntry<TKey, TValue> newLinkedEntry;
        if (previousEntry is not null)
        {
            newLinkedEntry = new LinkedEntry<TKey, TValue>(key, value, previousEntry.Next);
            previousEntry.Next = newLinkedEntry;
        }
        else
        {
            newLinkedEntry = new LinkedEntry<TKey, TValue>(key, value, _buckets[hash]);
            _buckets[hash] = newLinkedEntry;
        }
    }
    
    private LinkedEntry<TKey, TValue> GetEntry(TKey key)
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