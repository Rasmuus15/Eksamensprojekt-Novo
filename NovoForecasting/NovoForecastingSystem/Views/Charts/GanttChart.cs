using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using NovoForecastingSystem.Models.Enums;
using SkiaSharp;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NovoForecastingSystem.Views.Charts.GanttChart
{
    public class GanttViewModel : INotifyPropertyChanged
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
            SKColor.Parse("#FF0000"),
            SKColor.Parse("#00BFA5"),
            SKColor.Parse("#7C4DFF"),
            SKColor.Parse("#FF4081"),
            SKColor.Parse("#FF6D00"),
            SKColor.Parse("#AA00FF"),
            SKColor.Parse("#00C853"),
            SKColor.Parse("#2979FF"),
        };


        private ISeries[] _series;
        private Axis[] _xAxes;
        private Complexity _complexity;


        public ISeries[] Series
        {
            get => _series;
            private set { _series = value; OnPropertyChanged(); }
        }

        public Axis[] XAxes
        {
            get => _xAxes;
            private set { _xAxes = value; OnPropertyChanged(); }
        }

        public Axis[] YAxes { get; } = new[]
        {
            new Axis
            {
                Labels = TaskNames,
                TextSize = 14,
                ShowSeparatorLines = false
            }
        };

        public Complexity Complexity
        {
            get => _complexity;
            set
            {
                if (_complexity == value) return;
                _complexity = value;
                OnPropertyChanged();
                BuildChart(_complexity);
            }
        }

        public GanttViewModel() : this(Complexity.Low) { }

        public GanttViewModel(Complexity complexity)
        {
            _complexity = complexity;
            BuildChart(complexity);
        }

        private void BuildChart(Complexity complexity)
        {
            double[] starts;
            int[] durations;
            int maxLimit;

            switch (complexity)
            {
                case Complexity.Medium:
                    starts = StartsMedium;
                    durations = DurationsMedium;
                    maxLimit = MaxLimitMedium;
                    break;
                case Complexity.High:
                    starts = StartsHigh;
                    durations = DurationsHigh;
                    maxLimit = MaxLimitHigh;
                    break;
                default:
                    starts = StartsLow;
                    durations = DurationsLow;
                    maxLimit = MaxLimitLow;
                    break;
            }

            int count = TaskNames.Length;
            var seriesList = new List<ISeries>();

            seriesList.Add(new StackedRowSeries<double>
            {
                Values = starts,
                Fill = new SolidColorPaint(SKColors.Transparent),
                Stroke = null,
                DataLabelsPaint = null,
                IsHoverable = false
            });

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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}