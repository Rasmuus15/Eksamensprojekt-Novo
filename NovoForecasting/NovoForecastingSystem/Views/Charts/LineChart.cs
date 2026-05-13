using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Windows;

namespace NovoForecastingSystem.Views.Charts.LineChart
{

    public class LineChartViewModel
    {
        
        private static readonly double?[] BlueValues =
            { 5, 9, 12, 19, 27, 39, 50, 38, 25, 27, 15, 9};

        private static readonly double?[] PurpleValues =
            { 1, 5, 8, 11, 16, 26, 30, 23, 20, 23, 15, 7};

        private static readonly double?[] GreenValues =
            { 2, 6, 9, 10, 15, 22, 24, 24, 19, 15, 17, 11};

        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }
        public RectangularSection[] Sections { get; set; }

        public LineChartViewModel()
        {
            
            string[] labels = { "Jan", "Feb", "Mar",  "Apr",  "May",  "Jun",  "Jul",  "Aug",  "Sep",  "Oct",  "Nov",  "Dec"};
            for (int i = 0; i < 12; i++) ;

            Series = new ISeries[]
            {
                MakeLineSeries("Process Engineer",    BlueValues,   SKColors.CornflowerBlue),
                MakeLineSeries("Software Engineer",  PurpleValues, SKColors.MediumPurple),
                MakeLineSeries("Chemical Engineer",   GreenValues,  SKColors.MediumSeaGreen)
            };

            XAxes = new[]
            {
                new Axis
                {
                    Labels = labels,
                    TextSize = 12
                }
            };

            YAxes = new[]
            {
                new Axis
                {
                    MinLimit = 0,
                    MaxLimit = 60
                }
            };

            
            Sections = new[]
            {
                new RectangularSection   
                {
                    Yi = 7, Yj = 7,
                    Fill = new SolidColorPaint(SKColors.Pink) { StrokeThickness = 1.5f },
                    Stroke = new SolidColorPaint(SKColors.Pink) { StrokeThickness = 1.5f },
                },
                new RectangularSection   
                {
                    Yi = 5.5, Yj = 5.5,
                    Fill = new SolidColorPaint(SKColors.MediumAquamarine) { StrokeThickness = 1.5f },
                    Stroke = new SolidColorPaint(SKColors.MediumAquamarine) { StrokeThickness = 1.5f },
                },
            };
        }

        private static LineSeries<ObservablePoint> MakeLineSeries(
            string name, double?[] values, SKColor color)
        {
            var points = new ObservableCollection<ObservablePoint>();
            for (int i = 0; i < values.Length; i++)
            {
                points.Add(new ObservablePoint(i, values[i]));
            }

            return new LineSeries<ObservablePoint>
            {
                Name = name,
                Values = points,
                GeometrySize = 10,                          
                LineSmoothness = 100,                        
                EnableNullSplitting = false,
                Stroke = new SolidColorPaint(color) { StrokeThickness = 2 },
                Fill = null,                               
                GeometryFill = new SolidColorPaint(color),
                GeometryStroke = new SolidColorPaint(color),
            };
        }
    }
}


