using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace NovoForecastingSystem.Views.Charts.GanttChart
{
    public class GanttViewModel
    {
        private static readonly string[] TaskNames =
        {
        "Go Live", "Approval", "Testing", "Installation",
        "Manufacturing", "Detailed Design", "Basic Design", "Concept Design"
    };

        // start-uge per opgave (index 0 = Go Live, index 7 = Concept Design)
        private static readonly double[] Starts = { 76, 62, 57, 52, 21, 13, 5, 0 };
        private static readonly double[] Durations = { 5, 14, 5, 5, 31, 8, 8, 5 };


        private static readonly SKColor[] Colors =
        {
        SKColor.Parse("#FF0000"), // Go Live        - rød
        SKColor.Parse("#00BFA5"), // Approval       - teal
        SKColor.Parse("#7C4DFF"), // Testing        - lilla/blå
        SKColor.Parse("#FF4081"), // Installation   - pink
        SKColor.Parse("#FF6D00"), // Manufacturing  - orange
        SKColor.Parse("#AA00FF"), // Detailed Design- lilla
        SKColor.Parse("#00C853"), // Basic Design   - grøn
        SKColor.Parse("#2979FF"), // Concept Design - blå
    };

        public ISeries[] Series { get; }
        public Axis[] YAxes { get; }
        public Axis[] XAxes { get; }

        public GanttViewModel()
        {
            int count = TaskNames.Length;
            var seriesList = new List<ISeries>();

            // 1) Fælles transparent offset-serie
            seriesList.Add(new StackedRowSeries<double>
            {
                Values = Starts,
                Fill = new SolidColorPaint(SKColors.Transparent),
                Stroke = null,
                DataLabelsPaint = null,
                IsHoverable = false
            });

            // 2) Én farvet serie per opgave
            for (int i = 0; i < count; i++)
            {
                int taskIndex = i; // lokal kopi til closure
                double[] values = new double[count];
                values[taskIndex] = Durations[taskIndex];

                seriesList.Add(new StackedRowSeries<double>
                {
                    Values = values,
                    Name = TaskNames[taskIndex],
                    Fill = new SolidColorPaint(Colors[taskIndex]),
                    //Stroke = null,
                    DataLabelsPaint = new SolidColorPaint(SKColors.White),
                    DataLabelsSize = 13,
                    DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Middle,
                    DataLabelsFormatter = point =>
                        point.Model > 0 ? $"{point.Model}w" : string.Empty
                });
            }

            Series = seriesList.ToArray();

            YAxes = new[]
            {
            new Axis
            {
                Labels = TaskNames,
                TextSize = 14,
                ShowSeparatorLines = false
            }
        };

            XAxes = new[]
            {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = 81,
                TextSize = 12

                // Low comp = 81
                // Medium comp = 108
                // High comp = 137
            }
        };
        }
    }
}