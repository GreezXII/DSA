namespace HashTables;

public class ProbeSequenceStatistic
{
    private readonly List<int> _probeSequence = new();

    public IReadOnlyCollection<int> ProbeSequence => _probeSequence.AsReadOnly();

    public double? Average
    {
        get
        {
            if (_probeSequence.Count == 0)
                return null;
            return _probeSequence.Average();
        }
    }

    public double? Max
    {
        get
        {
            if (_probeSequence.Count == 0)
                return null;
            return _probeSequence.Max();
        }
    }

    public void AddProbe(int probe)
    {
        _probeSequence.Add(probe);
    }
}