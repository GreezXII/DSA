namespace Tests;

class SameHash
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
}