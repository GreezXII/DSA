namespace HashTables;

public class ProbeSequenceStatistic
{
    private readonly List<int> _probeSequence = new();

    public IReadOnlyCollection<int> ProbeSequence => _probeSequence.AsReadOnly();

    public double Average => _probeSequence.Average();
    public void AddProbe(int probe)
    {
        _probeSequence.Add(probe);
    }
}