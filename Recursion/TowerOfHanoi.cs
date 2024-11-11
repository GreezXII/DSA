namespace Recursion;

public class TowerOfHanoi
{
    private readonly Action<int, char, char, char> _action;
    
    public TowerOfHanoi(Action<int, char, char, char> action)
    {
        _action = action;   
    }
    
    public void Do(int level, char from, char to, char aux)
    {
        if (level == 0)
            return;
        
        Do(level - 1, from, aux, to);
        _action(level - 1, from, to, aux);
        Do(level - 1, aux, to, from);
    }
}