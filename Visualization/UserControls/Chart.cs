using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using DynamicData;
using ScottPlot.Avalonia;

namespace Visualization.UserControls;

public class Chart : AvaPlot
{
    #region HorizontalAxisLabel
    
    public static readonly DirectProperty<Chart, string> HorizontalAxisLabelProperty =
        AvaloniaProperty.RegisterDirect<Chart, string>(
            nameof(HorizontalAxisLabel),
            o => o.HorizontalAxisLabel,
            (o, v) => o.HorizontalAxisLabel = v);

    public string HorizontalAxisLabel
    {
        get => _horizontalAxisLabel;
        set => SetAndRaise(HorizontalAxisLabelProperty, ref _horizontalAxisLabel, value);
    }

    private string _horizontalAxisLabel = string.Empty;
    
    #endregion
    
    #region VerticalAxisLabel
    
    public static readonly DirectProperty<Chart, string> VerticalAxisLabelProperty =
        AvaloniaProperty.RegisterDirect<Chart, string>(
            nameof(VerticalAxisLabel),
            o => o.VerticalAxisLabel,
            (o, v) => o.VerticalAxisLabel = v);

    public string VerticalAxisLabel
    {
        get => _verticalAxisLabel;
        set => SetAndRaise(VerticalAxisLabelProperty, ref _verticalAxisLabel, value);
    }

    private string _verticalAxisLabel = string.Empty;

    #endregion
    
    #region PointsData
    
    public static readonly DirectProperty<Chart, ObservableCollection<Graph>> PointsDataProperty =
        AvaloniaProperty.RegisterDirect<Chart, ObservableCollection<Graph>>(
            nameof(PointsData),
            o => o.PointsData,
            (o, v) => o.PointsData = v);

    public ObservableCollection<Graph> PointsData
    {
        get => _pointsData;
        set => SetAndRaise(PointsDataProperty, ref _pointsData, value);
    }
    private ObservableCollection<Graph> _pointsData = new();
    
    #endregion

    public Chart()
    {
        UpdatePlot();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == PointsDataProperty)
            UpdatePlot();

        if (change.Property == HorizontalAxisLabelProperty)
            Plot.Axes.Bottom.Label.Text = HorizontalAxisLabel;
        
        if (change.Property == VerticalAxisLabelProperty)
            Plot.Axes.Left.Label.Text = VerticalAxisLabel;
    }

    private void UpdatePlot()
    {
        foreach (var graph in PointsData)
        {
            var dataX = graph.Data.Select(p => p.X).ToArray();
            var dataY = graph.Data.Select(p => p.Y).ToArray();
            var scatter = Plot.Add.Scatter(dataX, dataY);
            scatter.LegendText = graph.Name;
        }

        Refresh();
    }
}

public class Graph
{
    public Graph(string name, List<Point> data)
    {
        Name = name;
        Data = data;
    }

    public string Name { get; }
    public List<Point> Data { get; }
}