namespace HashTables;

public class Person
{
    private readonly string _name;

    public Person(string name) => _name = name;

    public override int GetHashCode() => 1;

    public override bool Equals(object? obj) =>
        obj is Person p && p._name == _name;
}