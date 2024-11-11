using System.Diagnostics;
using Recursion;

namespace Tests;

[TestClass]
public class RecursionTests
{
    [TestMethod]
    public void TowerOfHanoiTest()
    {
        var moves = new List<Move>();
        
        var toh = new TowerOfHanoi(
            (level, from, to, _) => moves.Add(new Move(level, from, to)));
        toh.Do(3, 'A', 'C', 'B');
        
        CollectionAssert.AreEqual(new List<Move>
        {
            new(0, 'A', 'C'),
            new(1, 'A', 'B'),
            new(0, 'C', 'B'),
            new(2, 'A', 'C'),
            new(0, 'B', 'A'),
            new(1, 'B', 'C'),
            new(0, 'A', 'C'),
        }, moves);
    }
    
    private record Move(int Level, char From, char To);
}