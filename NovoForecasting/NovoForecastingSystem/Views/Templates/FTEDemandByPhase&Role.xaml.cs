using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using NovoForecastingSystem.Models.Enums;
using NovoForecastingSystem.Models;
using NovoForecastingSystem.Repos;

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

        public static readonly DependencyProperty ProjectProperty =
            DependencyProperty.Register(
                nameof(Project),
                typeof(Project),
                typeof(FTEDemandByPhase_Role),
                new PropertyMetadata(null, OnProjectChanged));

        public Project Project
        {
            get => (Project)GetValue(ProjectProperty);
            set => SetValue(ProjectProperty, value);
        }

        private static void OnProjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FTEDemandByPhase_Role control)
            {
                control.GenerateDemands();
            }
        }

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

        private void GenerateDemands()
        {
            if (Project == null || FteDemands == null) return;
            
            ResourceRepo repo = new ResourceRepo();
            var currentCounts = repo.GetResourceCountByRoleForProject(Project.Id);
            
            FteDemands.Clear();

            foreach (JobRole role in Enum.GetValues(typeof(JobRole)))
            {
                var row = new FteDemandRow
                {
                    Role = FormatCamelCase(role.ToString())
                };
                
                string roleKey = role.ToString();
                int currentlyAssigned = currentCounts.ContainsKey(roleKey) ? currentCounts[roleKey] : 0;
                
                foreach (PhaseStage phase in Enum.GetValues(typeof(PhaseStage)))
                {
                    int required = CalculateDemand(role, phase, Project.ComplexityEnum);
                    
                    if (required > 0)
                    {
                        int assignedToThisPhase = Math.Min(currentlyAssigned, required);
                        currentlyAssigned -= assignedToThisPhase;
                        row[phase.ToString()] = $"{assignedToThisPhase} / {required}";
                    }
                    else
                    {
                        row[phase.ToString()] = "0 / 0"; 
                    }
                }
                
                FteDemands.Add(row);
            }
        }

        private int CalculateDemand(JobRole role, PhaseStage phase, Complexity complexity)
        {
            int baseDemand = 1;
            
            if (complexity == Complexity.Medium) baseDemand = 2;
            else if (complexity == Complexity.High) baseDemand = 3;
            
            // Adjust based on typical project lifecycle just as an example
            if (role == JobRole.ProjectManager)
            {
                return baseDemand; // needed everywhere
            }
            if (phase == PhaseStage.ConceptDesign)
            {
                if (role == JobRole.Designer) return baseDemand;
                return 0; // Other roles might not be needed in concept
            }
            
            return baseDemand;
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
            foreach (PhaseStage phase in Enum.GetValues(typeof(PhaseStage)))
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
