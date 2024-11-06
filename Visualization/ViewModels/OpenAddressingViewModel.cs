using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Avalonia.Input.TextInput;
using HashTables;
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

    public double? FillPercentage
    {
        get => _fillPercentage;
        set => this.RaiseAndSetIfChanged(ref _fillPercentage, value);
    }
    private double? _fillPercentage;
    
    public double? MaxProbe
    {
        get => _maxProbe;
        set => this.RaiseAndSetIfChanged(ref _maxProbe, value);
    }
    private double? _maxProbe;
    
    public double? AverageProbe
    {
        get => _averageProbe;
        set => this.RaiseAndSetIfChanged(ref _averageProbe, value);
    }
    private double? _averageProbe;

    public string Data
    {
        get => _data;
        set => this.RaiseAndSetIfChanged(ref _data, value);
    }
    private string _data = string.Empty;

    public int SelectionStart
    {
        get => _selectionStart;
        set => this.RaiseAndSetIfChanged(ref _selectionStart, value);
    }
    private int _selectionStart;

    public int SelectionEnd
    {
        get => _selectionEnd;
        set => this.RaiseAndSetIfChanged(ref _selectionEnd, value);
    }
    private int _selectionEnd;

    public OpenAddressingHashTable<int,int>? HashTable
    {
        get => _hashTable; 
        private set => this.RaiseAndSetIfChanged(ref _hashTable, value);
    }
    private OpenAddressingHashTable<int,int>? _hashTable;

    public ICommand CreateCommand { get; }
    public ICommand MakeItemCommand { get; }
    public ICommand InsertCommand { get; }
    public ICommand FindCommand { get; }
    public ICommand ClearCommand { get; }
    
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
            x => x.Min,
            x => x.Max,
            (item, min, max) => item >= min && item <= max);
        InsertCommand = ReactiveCommand.Create(ExecuteInsert, canExecuteInsertOrFind);
        FindCommand = ReactiveCommand.Create(ExecuteFind, canExecuteInsertOrFind);
        
        var canExecute = this.WhenAnyValue(
            x => x.HashTable,
            (OpenAddressingHashTable<int,int>? hashTable) => hashTable is not null);
        
        ClearCommand = ReactiveCommand.Create(Clear, canExecute);
        
        this.WhenAnyValue(x => x.Data)
            .Subscribe(x =>
            {
                FillPercentage = CalculateFillPercentage();
                MaxProbe = CalculateMaxProbe();
                AverageProbe = CalculateAveragePercentage();
            });
    }

    private void Clear()
    {
        HashTable = null;
        Data = string.Empty;
    }

    private double? CalculateFillPercentage()
    {
        if (HashTable is null)
            return null;

        if (HashTable.Size == 0)
            return 0.0;
        
        var value = (double)HashTable.Count / HashTable.Size * 100.0;
        return Math.Round(value, 2);
    }

    private double? CalculateMaxProbe()
    {
        var max = HashTable?.Statistic.Max;
        if (max is null)
            return null;
        return Math.Round(max.Value, 2);
    }

    private double? CalculateAveragePercentage()
    {
        var avg = HashTable?.Statistic.Average;
        if (avg is null)
            return null;
        return Math.Round(avg.Value, 2);
    }

    private void ExecuteCreate()
    {
        if (!Size.HasValue)
            return;
        
        var size = Size.Value;
        HashTable = new OpenAddressingHashTable<int, int>(size);
        Data = HashTable.ToString();
    }

    private void ExecuteMakeItem()
    {
        if (HashTable is null)
            return;
        
        if (!Max.HasValue)
            return;

        if (!Min.HasValue)
            return;

        if (!ItemsNumber.HasValue)
            return;
        
        var random = new Random();
        for (var i = 0; i < ItemsNumber; i++)
        {
            var number = random.Next(Min.Value, Max.Value);
            HashTable.TryAdd(number, number);
        }
        Data = HashTable.ToString();
    }

    private void ExecuteInsert()
    {
        if (HashTable is null)
            return;

        if (!Item.HasValue)
            return;

        HashTable.TryAdd(Item.Value, Item.Value);
        Data = HashTable.ToString();
    }

    private void ExecuteFind()
    {
        if (HashTable is null)
            return;

        if (!Item.HasValue)
            return;
        
        var isContain = HashTable.TryGet(Item.Value, out var number);
        if (!isContain)
            return;

        var value = Item.Value.ToString();
        var index = HashTable.ToString().IndexOf(value, StringComparison.CurrentCulture);
        SelectionEnd = index;
        SelectionStart = index + value.Length;
    }
}