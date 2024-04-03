using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RectangularRule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCanvasLoaded(object sender, RoutedEventArgs e)
        {
            DrawAll();
        }

        private void DrawAll()
        {
            MainCanvas.Children.Clear();
            double scale = 20;
            DrawFunc(scale);
            DrawPlot(scale);
        }

        private void DrawPlot(double scale)
        {
            var yLine = new Line()
            {
                X1 = 0,
                Y1 = 0,
                X2 = 0,
                Y2 = MainCanvas.ActualHeight,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            MainCanvas.Children.Add(yLine);

            for (int i = 0; i < MainCanvas.ActualHeight; i++)
            {
                var yMark = new Line()
                {
                    X1 = 0,
                    Y1 = i * scale,
                    X2 = 0.5 * scale,
                    Y2 = i * scale,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };
                MainCanvas.Children.Add(yMark);
            }

            var xLine = new Line()
            {
                X1 = 0,
                Y1 = 0,
                X2 = MainCanvas.ActualWidth,
                Y2 = 0,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            MainCanvas.Children.Add(xLine);

            for (int i = 0; i < MainCanvas.ActualWidth; i++)
            {
                var xMark = new Line()
                {
                    X1 = i * scale,
                    Y1 = 0,
                    X2 = i * scale,
                    Y2 = 0.5 * scale,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };
                MainCanvas.Children.Add(xMark);
            }
        }

        private void DrawFunc(double scale)
        {
            if (!double.TryParse(FromTB.Text, out double min))
                return;

            if (!double.TryParse(ToTB.Text, out double max))
                return;

            if (!int.TryParse(IntervalTB.Text, out int intervals))
                return;

            var type = GetShapeType();
            switch (type)
            {
                case ShapeType.Rectangle:
                    Rectangular(min, max, intervals, scale);
                    break;
                case ShapeType.Trapezoid:
                    Trapezoid(min, max, intervals, scale);
                    break;
                default:
                    break;
            }
        }

        private void Rectangular(double min, double max, int intervals, double scale)
        {
            var points = CalculateGraphPoints(min, max);
            if (points.Count < 2)
                return;
            double dx = (max - min) / intervals;
            var rects = CalculateRectangles(points, dx, intervals);
            double estArea = CalculateEstAreaRectangle(rects, dx);
            double area = CalculateArea();
            double err = estArea / area;
            ErrTb.Text = err.ToString();
            double rectStep = 0;
            foreach (var r in rects)
            {
                var rect = new Rectangle()
                {
                    Width = dx * scale,
                    Height = r.Y * scale,
                    Stroke = Brushes.Blue,
                    Fill = Brushes.LightGray
                };
                MainCanvas.Children.Add(rect);
                Canvas.SetLeft(rect, rectStep * scale);
                Canvas.SetTop(rect, 0);
                rectStep += dx;
            }
            for (int i = 1; i < points.Count; i++)
            {
                var startPoint = points[i - 1];
                var endPoint = points[i];
                var line = new Line()
                {
                    X1 = startPoint.X * scale,
                    Y1 = startPoint.Y * scale,
                    X2 = endPoint.X * scale,
                    Y2 = endPoint.Y * scale,
                    Stroke = Brushes.Green,
                    StrokeThickness = 1
                };
                MainCanvas.Children.Add(line);
            }
        }

        private void Trapezoid(double min, double max, int intervals, double scale)
        {
            var points = CalculateGraphPoints(min, max);
            if (points.Count < 2)
                return;
            double dx = (max - min) / intervals;
            var trapezoids = CalculateTrapezoids(points, dx, intervals);
            double estArea = CalculateEstAreaTrapezoid(trapezoids, dx);
            double area = CalculateArea();
            double err = estArea / area;
            ErrTb.Text = err.ToString();
            foreach (var t in trapezoids)
            {
                var poly = new Polygon()
                {
                    Points = TrapezoidToPointCollection(t, dx, scale),
                    Stroke = Brushes.Blue,
                    Fill = Brushes.LightGray
                };
                MainCanvas.Children.Add(poly);
            }
            for (int i = 1; i < points.Count; i++)
            {
                var startPoint = points[i - 1];
                var endPoint = points[i];
                var line = new Line()
                {
                    X1 = startPoint.X * scale,
                    Y1 = startPoint.Y * scale,
                    X2 = endPoint.X * scale,
                    Y2 = endPoint.Y * scale,
                    Stroke = Brushes.Green,
                    StrokeThickness = 1
                };
                MainCanvas.Children.Add(line);
            }
        }

        private static List<Point> CalculateGraphPoints(double min, double max)
        {
            var result = new List<Point>();
            while (min < max)
            {
                double x = min;
                double func = 1 + x + Math.Sin(2 * min);
                min += 0.1;
                result.Add(new Point(x, func));
            }
            return result;
        }

        private static List<Point> CalculateRectangles(List<Point> points, double dx, int intervals)
        {
            double x = 0;

            var result = new List<Point>();
            for (int i = 0; i < intervals; i++)
            {
                var point = points.FirstOrDefault(p => p.X >= x);
                if (point is null)
                    return result;

                result.Add(new Point(point.X, point.Y));
                x += dx;
            }
            return result;
        }

        private static List<Trapezoid> CalculateTrapezoids(List<Point> points, double dx, int intervals)
        {
            double x = 0;

            var result = new List<Trapezoid>();
            for (int i = 0; i < intervals; i++)
            {
                var point1 = points.LastOrDefault(p => p.X <= x);
                var point2 = points.LastOrDefault(p => p.X <= x + dx);
                if (point1 is null || point2 is null)
                    return result;

                result.Add(new Trapezoid(point1.X, point1.Y, point2.Y));
                x += dx;
            }
            return result;
        }

        private double CalculateEstAreaRectangle(List<Point> points, double dx)
        {
            double area = points?.Sum(p => dx * p.Y) ?? 0.0;
            EstAreaTB.Text = area.ToString();
            return area;
        }

        private double CalculateEstAreaTrapezoid(List<Trapezoid> points, double dx)
        {
            double area = points?.Sum(t => dx * (t.Y1 + t.Y2) / 2) ?? 0.0;
            EstAreaTB.Text = area.ToString();
            return area;
        }

        private double CalculateArea()
        {
            if (!double.TryParse(FromTB.Text, out double min))
                return 0.0;

            if (!double.TryParse(ToTB.Text, out double max))
                return 0.0;

            double Fa = Math.Pow(Math.Sin(min), 2) + Math.Pow(min, 2) / 2 + min;
            double Fb = Math.Pow(Math.Sin(max), 2) + Math.Pow(max, 2) / 2 + max;
            double area = Fb - Fa;
            TrueAreaTB.Text = area.ToString();
            return area;
        }

        private ShapeType? GetShapeType()
        {
            var type = typeSP.Children.OfType<RadioButton>()
                .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);

            switch (type?.Content?.ToString())
            {
                case "Rectangle":
                    return ShapeType.Rectangle;
                case "Trapezoid":
                    return ShapeType.Trapezoid;
                default:
                    return null;
            }
        }

        private static PointCollection TrapezoidToPointCollection(Trapezoid trapezoid, double dx, double scale)
        {
            var q = new PointCollection()
            {
                new System.Windows.Point(trapezoid.X * scale, 0),
                new System.Windows.Point(trapezoid.X * scale, trapezoid.Y1 * scale),
                new System.Windows.Point((trapezoid.X + dx) * scale, trapezoid.Y2 * scale),
                new System.Windows.Point((trapezoid.X + dx) * scale, 0)
            };
            return q;
        }

        private void OnIntegrate(object sender, RoutedEventArgs e)
        {
            DrawAll();
        }
    }

    class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }

    class Trapezoid
    {
        public Trapezoid(double x, double y1, double y2)
        {
            X = x;
            Y1 = y1;
            Y2 = y2;
        }

        public double X { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }
    }

    enum ShapeType
    {
        Rectangle,
        Trapezoid
    }
}