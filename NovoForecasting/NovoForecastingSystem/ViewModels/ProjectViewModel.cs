using NovoForecastingSystem.Commands;
using NovoForecastingSystem.Models;
using NovoForecastingSystem.Repos;
using NovoForecastingSystem.Services;
using NovoForecastingSystem.Stores;
using NovoForecastingSystem.Views;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace NovoForecastingSystem.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        public ICommand NavigateToDashboardViewCommand { get; }
        public ICommand AddResourceCommand { get; }
        public ICommand EditProjectCommand { get; }

        private Project _currentProject;
        public Project CurrentProject
        {
            get => _currentProject;
            set { _currentProject = value; OnPropertyChanged(); }
        }

        public ProjectViewModel(Project project, NavigationStore navigationStore)
        {
            CurrentProject = project;

            NavigateToDashboardViewCommand = new NavigateCommand(new NavigationService(navigationStore, () => new DashBoardViewModel(navigationStore)));
            AddResourceCommand = new AddResourceCommand();
            EditProjectCommand = new EditProjectCommand();
        }

        public IEnumerable<NovoForecastingSystem.Models.Enums.PhaseStage> PhaseStages => Enum.GetValues<NovoForecastingSystem.Models.Enums.PhaseStage>();
        public IEnumerable<NovoForecastingSystem.Models.Enums.JobRole> JobRoles => Enum.GetValues<NovoForecastingSystem.Models.Enums.JobRole>();

        private NovoForecastingSystem.Models.Enums.PhaseStage? _selectedPhase;
        public NovoForecastingSystem.Models.Enums.PhaseStage? SelectedPhase
        {
            get => _selectedPhase;
            set { _selectedPhase = value; OnPropertyChanged(); }
        }

        private NovoForecastingSystem.Models.Enums.JobRole? _selectedRole;
        public NovoForecastingSystem.Models.Enums.JobRole? SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
                UpdateEmails();
            }
        }

        private List<string> _emails;
        public List<string> Emails
        {
            get => _emails;
            set { _emails = value; OnPropertyChanged(); }
        }

        private string _selectedEmail;
        public string SelectedEmail
        {
            get => _selectedEmail;
            set { _selectedEmail = value; OnPropertyChanged(); }
        }

        private void UpdateEmails()
        {
            if (SelectedRole.HasValue)
            {
                ResourceRepo repo = new ResourceRepo();
                Emails = repo.PrintEmail(SelectedRole.Value.ToString());
            }
            else
            {
                Emails = new List<string>();
            }
        }

        public void AddResource()
        {
            if (!string.IsNullOrEmpty(SelectedEmail) && CurrentProject != null)
            {
                try
                {
                    ResourceRepo repo = new ResourceRepo();
                    repo.UpdateResourceProject(SelectedEmail, CurrentProject.Id);
                    MessageBox.Show("Resource added to project successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    SelectedEmail = null;
                    SelectedRole = null;
                    SelectedPhase = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an email.");
            }
        }

        public void EditProject()
        {
            MessageBox.Show("Test");
        }

        
    }
}
