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
            { null, 3, 3, null, null, null, null, null, null, null, null, null, null, null, 3, null, 2, null, null, null, null };

        private static readonly double?[] PurpleValues =
            { 2, null, 2, 2, null, 4, 4, 4, 4, 4, 4, 4, 4, null, 2, null, null, null, 2, null, 2 };

        private static readonly double?[] GreenValues =
            { 1, null, null, null, 3, null, 3, 67, null, 3, 3, 3, null, null, 1, 1, null, 1, 1, 1, 1 };

        private static readonly double?[] RedValues =
            { 0, null, null, null, null, null, null, null, null, null, null, null, null, null, 0, null, 0, 0, null, 0, 0 };

        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }
        public RectangularSection[] Sections { get; set; }

        public LineChartViewModel()
        {
            // Ugedage som X-labels
            var labels = new string[21];
            for (int i = 0; i < 21; i++) labels[i] = "Mon";

            Series = new ISeries[]
            {
                MakeLineSeries("Blå",    BlueValues,   SKColors.CornflowerBlue),
                MakeLineSeries("Lilla",  PurpleValues, SKColors.MediumPurple),
                MakeLineSeries("Grøn",   GreenValues,  SKColors.MediumSeaGreen),
                MakeLineSeries("Rød",    RedValues,    SKColors.Crimson),
            };

            XAxes = new[]
            {
                new Axis
                {
                    Labels = labels,
                    TextSize = 11,
                }
            };

            YAxes = new[]
            {
                new Axis
                {
                    MinLimit = -1,
                    MaxLimit = 8,
                    IsVisible = false  // Skjul Y-akse som i billedet
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


