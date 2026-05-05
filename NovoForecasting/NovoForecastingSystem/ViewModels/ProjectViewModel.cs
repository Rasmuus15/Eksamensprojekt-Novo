using NovoForecastingSystem.Commands;
using NovoForecastingSystem.Models;
using NovoForecastingSystem.Models.Enums;
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
using static SkiaSharp.HarfBuzz.SKShaper;

namespace NovoForecastingSystem.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        public ICommand NavigateToDashboardViewCommand { get; }
        public ICommand AddResourceCommand { get; }
        public ICommand EditProjectCommand { get; }
        public ICommand DeleteProjectCommand { get; }
        private readonly NavigationStore _navigationStore;

        private Project _currentProject;
        public Project CurrentProject
        {
            get => _currentProject;
            set { _currentProject = value; OnPropertyChanged(); }
        }

        // Add Resource Window properties and backing fields
        public IEnumerable<PhaseStage> PhaseStages => Enum.GetValues<PhaseStage>();
        public IEnumerable<JobRole> JobRoles => Enum.GetValues<JobRole>();

        private PhaseStage? _selectedPhase;
        public PhaseStage? SelectedPhase
        {
            get => _selectedPhase;
            set { _selectedPhase = value; OnPropertyChanged(); }
        }

        private JobRole? _selectedRole;
        public JobRole? SelectedRole
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

        // Edit project properties and backing fields
        public string ProjectName
        {
            get => CurrentProject.ProjectName;
            set { CurrentProject.ProjectName = value; OnPropertyChanged(); }
        }
        public DateTime? StartDate
        {
            get => CurrentProject.StartDate.ToDateTime(TimeOnly.FromDateTime(DateTime.Now));
            set { CurrentProject.StartDate = DateOnly.FromDateTime(value.Value); OnPropertyChanged(); }
        }
        public Complexity? SelectedComplexity
        {
            get => CurrentProject.ComplexityEnum;
            set { CurrentProject.ComplexityEnum = value.Value; OnPropertyChanged(); }
        }
        public PhaseStage SelectedPhaseEditProject
        {
            get => CurrentProject.Phase.phaseStage;
            set { CurrentProject.Phase.phaseStage = value; OnPropertyChanged(); }
        }

        public ProjectViewModel(Project project, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            CurrentProject = project;

            NavigateToDashboardViewCommand = new NavigateCommand(new NavigationService(navigationStore, () => new DashBoardViewModel(navigationStore)));
            AddResourceCommand = new AddResourceCommand();
            EditProjectCommand = new EditProjectCommand();
            DeleteProjectCommand = new DeleteProjectCommand();
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
            try
            {
                if (string.IsNullOrWhiteSpace(ProjectName))
                {
                    MessageBox.Show("Please enter a project name.");
                    return;
                }

                if (StartDate.HasValue && SelectedComplexity.HasValue)
                {
                    ProjectRepo projectRepo = new ProjectRepo();
                    projectRepo.EditProject(CurrentProject);
                }

                // Updates the view trough the OnPropertyChanged event in BaseViewModel
                OnPropertyChanged(nameof(CurrentProject));

                MessageBox.Show("Project edited");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        public void DeleteProject()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this project", "Delete Project", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                ProjectRepo projectRepo = new ProjectRepo();
                projectRepo.DeleteProject(CurrentProject);

                _navigationStore.CurrentViewModel = new DashBoardViewModel(_navigationStore);

                MessageBox.Show($"Deleted Project {CurrentProject.ProjectName}");
            }
        }
    }
}
