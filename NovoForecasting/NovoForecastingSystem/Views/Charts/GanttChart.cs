using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using NovoForecastingSystem.Models.Enums;
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

        private static readonly double[] StartsLow = { 76, 62, 57, 52, 21, 13, 5, 0 };
        private static readonly double[] StartsMedium = { 101, 82, 75, 69, 27, 17, 7, 0 };
        private static readonly double[] StartsHigh = { 128, 104, 95, 87, 35, 22, 9, 0 };

        private static readonly int[] DurationsLow = { 5, 14, 5, 5, 31, 8, 8, 5 };
        private static readonly int[] DurationsMedium = { 7, 19, 7, 6, 42, 10, 10, 7 };
        private static readonly int[] DurationsHigh = { 9, 24, 9, 8, 52, 13, 13, 9 };

        private static readonly int MaxLimitLow = 81;
        private static readonly int MaxLimitMedium = 108;
        private static readonly int MaxLimitHigh = 137;

        private static readonly SKColor[] Colors =
        {
            SKColor.Parse("#FF0000"), // Go Live         - rød
            SKColor.Parse("#00BFA5"), // Approval        - teal
            SKColor.Parse("#7C4DFF"), // Testing         - lilla/blå
            SKColor.Parse("#FF4081"), // Installation    - pink
            SKColor.Parse("#FF6D00"), // Manufacturing   - orange
            SKColor.Parse("#AA00FF"), // Detailed Design - lilla
            SKColor.Parse("#00C853"), // Basic Design    - grøn
            SKColor.Parse("#2979FF"), // Concept Design  - blå
        };

        public ISeries[] Series { get; }
        public Axis[] YAxes { get; }
        public Axis[] XAxes { get; }

        public GanttViewModel() : this(Complexity.Low) { }

        public GanttViewModel(Complexity complexity)
        {
            double[] starts;
            int[] durations;
            int maxLimit;

            if (complexity == Complexity.Low)
            {
                starts = StartsLow;
                durations = DurationsLow;
                maxLimit = MaxLimitLow;
            }
            else if (complexity == Complexity.Medium)
            {
                starts = StartsMedium;
                durations = DurationsMedium;
                maxLimit = MaxLimitMedium;
            }
            else
            {
                starts = StartsHigh;
                durations = DurationsHigh;
                maxLimit = MaxLimitHigh;
            }

            int count = TaskNames.Length;
            var seriesList = new List<ISeries>();

            // 1) Transparent offset series to push bars to their start positions
            seriesList.Add(new StackedRowSeries<double>
            {
                Values = starts,
                Fill = new SolidColorPaint(SKColors.Transparent),
                Stroke = null,
                DataLabelsPaint = null,
                IsHoverable = false
            });

            // 2) One colored series per task
            for (int i = 0; i < count; i++)
            {
                int taskIndex = i;
                double[] values = new double[count];
                values[taskIndex] = durations[taskIndex];

                seriesList.Add(new StackedRowSeries<double>
                {
                    Values = values,
                    Name = TaskNames[taskIndex],
                    Fill = new SolidColorPaint(Colors[taskIndex]),
                    Stroke = null,
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
                    MaxLimit = maxLimit,
                    TextSize = 12
                }
            };
        }
    }
}