using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using NovoForecastingSystem.Models.Enums;

namespace NovoForecastingSystem.Views.Templates
{
    public class FteDemandRow
    {
        public string Role { get; set; }

        private Dictionary<string, string> _phases = new Dictionary<string, string>();

        public string this[string phase]
        {
            get => _phases.ContainsKey(phase) ? _phases[phase] : string.Empty;
            set => _phases[phase] = value;
        }
    }

    /// <summary>
    /// Interaction logic for FTEDemandByPhase_Role.xaml
    /// </summary>
    public partial class FTEDemandByPhase_Role : UserControl
    {
        public ObservableCollection<FteDemandRow> FteDemands { get; set; } = new ObservableCollection<FteDemandRow>();

        public static List<string> AvailableRoles { get; } = new List<string>();

        static FTEDemandByPhase_Role()
        {
            foreach (JobRole role in Enum.GetValues(typeof(JobRole)))
            {
                AvailableRoles.Add(Regex.Replace(role.ToString(), "([A-Z])", " $1").Trim());
            }
        }

        public FTEDemandByPhase_Role()
        {
            InitializeComponent();
            LoadColumns();
            LoadDefaultRoles();
            if (dgFTEDemand != null)
            {
                dgFTEDemand.ItemsSource = FteDemands;
            }
        }

        private void LoadColumns()
        {
            if (dgFTEDemand == null) return;

            // Remove existing static phase columns if any, keep the Role column (index 0)
            while (dgFTEDemand.Columns.Count > 1)
            {
                dgFTEDemand.Columns.RemoveAt(1);
            }

            // Create columns based on the Phase enum
            foreach (Phase phase in Enum.GetValues(typeof(Phase)))
            {
                string phaseName = phase.ToString();
                string headerName = FormatCamelCase(phaseName);

                var column = new DataGridTextColumn
                {
                    Header = headerName,
                    Binding = new Binding($"[{phaseName}]"),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };
                dgFTEDemand.Columns.Add(column);
            }
        }

        private void LoadDefaultRoles()
        {
            FteDemands = new ObservableCollection<FteDemandRow>();

            // Generate rows based on JobRole enum
            foreach (JobRole role in Enum.GetValues(typeof(JobRole)))
            {
                var row = new FteDemandRow
                {
                    Role = FormatCamelCase(role.ToString())
                };
                FteDemands.Add(row);
            }
        }

        private string FormatCamelCase(string str)
        {
            return Regex.Replace(str, "([A-Z])", " $1").Trim();
        }
    }
}
