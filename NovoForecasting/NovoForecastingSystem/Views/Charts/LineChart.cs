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
        // Dato-punkter pr. serie (21 Mandage)
        private static readonly double?[] BlueValues =
            { null, 3, 3, null, null, null, null, null, null, null, null};

        private static readonly double?[] PurpleValues =
            { 2, null, 2, 2, null, 4, 4, 4, 4, 4, 4, 4};

        private static readonly double?[] GreenValues =
            { 1, null, null, null, 3, null, 3, 3, null};

        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }
        public RectangularSection[] Sections { get; set; }

        public LineChartViewModel()
        {
            // Ugedage som X-labels
            string[] labels = { "Jan", "Feb", "Mar",  "Apr",  "May",  "Jun",  "Jul",  "Aug",  "Sep",  "Oct",  "Nov",  "Dec"};
            for (int i = 0; i < 12; i++) ;

            Series = new ISeries[]
            {
                MakeLineSeries("Process Engineer",    BlueValues,   SKColors.CornflowerBlue),
                MakeLineSeries("Automation Engineer",  PurpleValues, SKColors.MediumPurple),
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
                    MaxLimit = 20
                }
            };

            // Threshold-linjer (vandret) som i billedet
            Sections = new[]
            {
                new RectangularSection   // Lyserød linje øverst
                {
                    Yi = 7, Yj = 7,
                    Fill = new SolidColorPaint(SKColors.Pink) { StrokeThickness = 1.5f },
                    Stroke = new SolidColorPaint(SKColors.Pink) { StrokeThickness = 1.5f },
                },
                new RectangularSection   // Grøn/teal linje
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
                GeometrySize = 10,                          // Prikstørrelse
                LineSmoothness = 100,                         // Lige linjer
                EnableNullSplitting = false,
                Stroke = new SolidColorPaint(color) { StrokeThickness = 2 },
                Fill = null,                                // Ingen fyld under linjen
                GeometryFill = new SolidColorPaint(color),
                GeometryStroke = new SolidColorPaint(color),
            };
        }
    }
}


