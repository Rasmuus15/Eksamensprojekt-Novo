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
    public class FteDemandRow : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private string _role;
        public string Role
        {
            get => _role;
            set
            {
                if (_role != value)
                {
                    _role = value;
                    PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Role)));
                }
            }
        }

        private Dictionary<string, string> _phases = new Dictionary<string, string>();

        public string this[string phase]
        {
            get => _phases.ContainsKey(phase) ? _phases[phase] : string.Empty;
            set
            {
                if (!_phases.ContainsKey(phase) || _phases[phase] != value)
                {
                    _phases[phase] = value;
                    PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(System.Windows.Data.Binding.IndexerName));
                }
            }
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

        public void GenerateDemands()
        {
            if (Project == null || FteDemands == null) return;
            
            ResourceRepo repo = new ResourceRepo();
            var currentCounts = repo.GetResourceCountByRoleForProject(Project.Id);
            
            if (FteDemands.Count == 0) LoadDefaultRoles();

            foreach (var row in FteDemands)
            {
                string roleName = row.Role;
                // e.g. "Process Engineer" -> "ProcessEngineer"
                string roleKey = roleName.Replace(" ", ""); 
                
                // fallback parse
                if (!Enum.TryParse(roleKey, out JobRole parsedRole))
                {
                    continue;
                }

                int currentlyAssigned = currentCounts.ContainsKey(roleKey) ? currentCounts[roleKey] : 0;
                var phaseDemands = new Dictionary<string, int>();
                var assignedDemands = new Dictionary<string, int>();
                
                // Pass 1: Allocate up to the required amount so they fill phases properly
                foreach (PhaseStage phase in Enum.GetValues(typeof(PhaseStage)))
                {
                    int required = CalculateDemand(parsedRole, phase, Project.ComplexityEnum);
                    phaseDemands[phase.ToString()] = required;
                    
                    int assignedToThisPhase = Math.Min(currentlyAssigned, required);
                    assignedDemands[phase.ToString()] = assignedToThisPhase;
                    currentlyAssigned -= assignedToThisPhase;
                }

                // Pass 2: Allocate any remaining/extra people to phases that need them
                if (currentlyAssigned > 0)
                {
                    bool assignedExtra = false;
                    foreach (PhaseStage phase in Enum.GetValues(typeof(PhaseStage)))
                    {
                        if (currentlyAssigned > 0 && phaseDemands[phase.ToString()] > 0)
                        {
                            assignedDemands[phase.ToString()] += currentlyAssigned;
                            currentlyAssigned = 0;
                            assignedExtra = true;
                            break; // Stop after assigning extras to the first required phase
                        }
                    }

                    // Fallback: if no phase required this role but we have them, assign to first phase
                    if (!assignedExtra && currentlyAssigned > 0)
                    {
                        var firstPhase = ((PhaseStage[])Enum.GetValues(typeof(PhaseStage)))[0].ToString();
                        assignedDemands[firstPhase] += currentlyAssigned;
                        currentlyAssigned = 0;
                    }
                }

                // Finally write to the UI row
                foreach (PhaseStage phase in Enum.GetValues(typeof(PhaseStage)))
                {
                    row[phase.ToString()] = $"{assignedDemands[phase.ToString()]} / {phaseDemands[phase.ToString()]}";
                }
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
