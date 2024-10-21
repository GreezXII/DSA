namespace Tests;

class SameHash : IComparable<SameHash>
{
    public SameHash(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    
    public override int GetHashCode()
    {
        return 0;
    }

    public override bool Equals(object? obj) => obj is SameHash hash && Name == hash.Name;

    public int CompareTo(SameHash? other)
    {
        if (ReferenceEquals(this, other)) 
            return 0;
        if (other is null) 
            return 1;
        return string.Compare(Name, other.Name, StringComparison.Ordinal);
    }
}