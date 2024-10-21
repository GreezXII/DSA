using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using ScottPlot.Avalonia;

namespace Visualization.UserControls;

public class Chart : AvaPlot
{
    public static readonly DirectProperty<Chart, ObservableCollection<List<Point>>> PointsDataProperty =
        AvaloniaProperty.RegisterDirect<Chart, ObservableCollection<List<Point>>>(
            nameof(PointsData),
            o => o.PointsData,
            (o, v) => o.PointsData = v);
    public ObservableCollection<List<Point>> PointsData
    {
        get => _pointsData;
        set => SetAndRaise(PointsDataProperty, ref _pointsData, value);
    }
    private ObservableCollection<List<Point>> _pointsData = new();

    public Chart()
    {
        UpdatePlot();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == PointsDataProperty)
            UpdatePlot();
    }

    private void UpdatePlot()
    {
        foreach (var graph in PointsData)
        {
            var dataX = graph.Select(p => p.X).ToArray();
            var dataY = graph.Select(p => p.Y).ToArray();
            Plot.Add.Scatter(dataX, dataY);
        }
        Refresh();
    }
}