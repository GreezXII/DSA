using ReactiveUI;

namespace Visualization.ViewModels;

public class OpenAddressingViewModel : ViewModelBase
{
    public override string Title => "Open Addressing";

    public int Size
    {
        get => _size; 
        set => this.RaiseAndSetIfChanged(ref _size, value);
    }
    private int _size = 101;

    public int Min
    {
        get => _min;
        set => this.RaiseAndSetIfChanged(ref _min, value);
    }
    private int _min = 100;
    
    public int Max
    {
        get => _max;
        set => this.RaiseAndSetIfChanged(ref _max, value);
    }
    private int _max = 999;

    public int ItemsNumber
    {
        get => _itemsNumber;
        set => this.RaiseAndSetIfChanged(ref _itemsNumber, value);
    }
    private int _itemsNumber = 50;

    public int Item
    {
        get => _item;
        set => this.RaiseAndSetIfChanged(ref _item, value);
    }
    private int _item = 123;

    public double FillPercentage
    {
        get => _fillPercentage;
        set => this.RaiseAndSetIfChanged(ref _fillPercentage, value);
    }
    private double _fillPercentage;
    
    public double MaxProbe
    {
        get => _maxProbe;
        set => this.RaiseAndSetIfChanged(ref _maxProbe, value);
    }
    private double _maxProbe;
    
    public double AverageProbe
    {
        get => _averageProbe;
        set => this.RaiseAndSetIfChanged(ref _averageProbe, value);
    }
    private double _averageProbe;

}