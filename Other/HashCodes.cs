namespace Other;

public class A
{
    public A(int foo, string bar, double fooBar)
    {
        Foo = foo;
        Bar = bar;
        FooBar = fooBar;
    }

    public int Foo { get; }
    public string Bar { get; }
    public double FooBar { get; }

    public override bool Equals(object? obj)
    {
        if (obj is A a)
            return Foo == a.Foo && Bar == a.Bar;
        return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Foo * 397) ^ Bar.GetHashCode();
        }
    }
}

public class B
{
    public B(int foo, string bar, double fooBar)
    {
        Foo = foo;
        Bar = bar;
        FooBar = fooBar;
    }

    public int Foo { get; }
    public string Bar { get; }
    public double FooBar { get; }

    public override bool Equals(object? obj)
    {
        if (obj is A a)
            return Foo == a.Foo && Bar == a.Bar;
        return false;
    }

    /*
     No hash code func overrided
    public override int GetHashCode()
    {
        unchecked
        {
            return (Foo * 397) ^ Bar.GetHashCode();
        }
    }*/
}