using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using ReactiveUI;

namespace Visualization.ViewModels;

public class OpenAddressingViewModel : ViewModelBase
{
    private const string ShouldBeIntegerError = "Value should be integer.";
    private const string RangeError = "Value must be between {1} and {2}.";
    public override string Title => "Open Addressing";

    [Required(ErrorMessage = ShouldBeIntegerError)]
    [Range(1, 999, ErrorMessage = RangeError)]
    public int? Size
    {
        get => _size;
        set => this.RaiseAndSetIfChanged(ref _size, value);
    }
    private int? _size = 101;

    [Required(ErrorMessage = ShouldBeIntegerError)]
    [Range(1, 999, ErrorMessage = RangeError)]
    public int? Min
    {
        get => _min;
        set => this.RaiseAndSetIfChanged(ref _min, value);
    }
    private int? _min = 100;
    
    [Required(ErrorMessage = ShouldBeIntegerError)]
    [Range(1, 999, ErrorMessage = RangeError)]
    public int? Max
    {
        get => _max;
        set => this.RaiseAndSetIfChanged(ref _max, value);
    }
    private int? _max = 999;
    
    [Required(ErrorMessage = ShouldBeIntegerError)]
    [Range(1, 999, ErrorMessage = RangeError)]
    public int? ItemsNumber
    {
        get => _itemsNumber;
        set => this.RaiseAndSetIfChanged(ref _itemsNumber, value);
    }
    private int? _itemsNumber = 50;

    [Required(ErrorMessage = ShouldBeIntegerError)]
    [Range(1, 999, ErrorMessage = RangeError)]
    public int? Item
    {
        get => _item;
        set => this.RaiseAndSetIfChanged(ref _item, value);
    }
    private int? _item = 123;

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

    public string Data
    {
        get => _data;
        set => this.RaiseAndSetIfChanged(ref _data, value);
    }
    private string _data = "Data";
    
    public ICommand CreateCommand { get; }
    public ICommand MakeItemCommand { get; }
    public ICommand InsertCommand { get; }
    public ICommand FindCommand { get; }

    public OpenAddressingViewModel()
    {
        var canExecuteCreate = this.WhenAnyValue(
            x => x.Size, 
            size => size.HasValue);
        CreateCommand = ReactiveCommand.Create(ExecuteCreate, canExecuteCreate);
        
        var canExecuteMakeItem = this.WhenAnyValue(
            x => x.Min, 
            x => x.Max,
            x => x.ItemsNumber,
            (min, max, itemsNumber) => min.HasValue && max.HasValue && itemsNumber.HasValue);
        MakeItemCommand = ReactiveCommand.Create(ExecuteMakeItem, canExecuteMakeItem);
        
        var canExecuteInsertOrFind = this.WhenAnyValue(
            x => x.Item,
            item => item.HasValue);
        InsertCommand = ReactiveCommand.Create(ExecuteInsert, canExecuteInsertOrFind);
        FindCommand = ReactiveCommand.Create(ExecuteFind, canExecuteInsertOrFind);
    }
    
    private void ExecuteCreate()
    {
        Data += "\nExecute create";
    }

    private void ExecuteMakeItem()
    {
        Data += "\nExecute make item";
    }

    private void ExecuteInsert()
    {
        Data += "\nExecute insert";
    }

    private void ExecuteFind()
    {
        Data += "\nExecute find";
    }
}