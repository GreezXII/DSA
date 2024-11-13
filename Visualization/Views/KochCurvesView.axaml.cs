using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;

namespace Visualization.Views;

public partial class KochCurvesView : UserControl
{
    public KochCurvesView()
    {
        InitializeComponent();

        Task.Run(async () =>
        {
            await Task.Delay(100);
            if (DepthControl.Value is not null)
                Dispatcher.UIThread.Invoke(() => Draw((int)DepthControl.Value));
        });
    }

    private void Draw(int depth)
    {
        var height = MainCanvas.Bounds.Height / 2;
        var width = MainCanvas.Bounds.Width / 2;
        const double length = 400.0;

        var startX = width;
        var startY = height;
        var startPoint = new Point(startX, startY);

        DrawKochCurves(depth, startPoint, 0, length, out var p2);
        DrawKochCurves(depth, p2, 120, length, out var p3);
        DrawKochCurves(depth, p3, 240, length, out var p4);
        
        var delta = startPoint.X - p2.X;
        MainScrollViewer.Offset = new Point(startPoint.X - delta, p2.Y + delta);
    }

    private void DrawLine(Point startPoint, Point endPoint)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            var line = new Line
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                Stroke = Brushes.Red,
                StrokeThickness = 5.0
            };
            MainCanvas.Children.Add(line);
        });
        Console.WriteLine($"DrawLine. Start: {startPoint.X}, {startPoint.Y}. End: {endPoint.X}, {endPoint.Y}");
    }

    private void DrawKochCurves(int depth, Point p1, double angle, double length, out Point endPoint)
    {
        if (depth == 0)
        {
            var p2 = GetEndPoint(p1, length, angle);
            DrawLine(p1, p2);
            endPoint = p2;
        }
        else
        {
            var p2 = GetEndPoint(p1, length / 3, angle);
            var p3 = GetEndPoint(p2, length / 3, angle - 60);
            var p4 = GetEndPoint(p3, length / 3, angle + 60);
            DrawKochCurves(depth - 1, p1, angle, length / 3, out endPoint);
            DrawKochCurves(depth - 1, p2, angle - 60, length / 3, out endPoint);
            DrawKochCurves(depth - 1, p3, angle + 60, length / 3, out endPoint);
            DrawKochCurves(depth - 1, p4, angle, length / 3, out endPoint);
        }
    }

    private Point GetEndPoint(Point startPoint, double length, double angle)
    {
        var a = Math.PI * angle / 180.0;
        return new Point(
            startPoint.X + length * Math.Cos(a),
            startPoint.Y + length * Math.Sin(a));
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DepthControl.Value is null) 
            return;
        MainCanvas.Children.Clear();
        Draw((int)DepthControl.Value);
    }
}