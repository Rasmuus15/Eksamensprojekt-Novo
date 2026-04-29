using NovoForecastingSystem.Commands;
using NovoForecastingSystem.Models;
using NovoForecastingSystem.Models.Enums;
using NovoForecastingSystem.Repos;
using NovoForecastingSystem.Services;
using NovoForecastingSystem.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ComplexityEnum = NovoForecastingSystem.Models.Enums.Complexity;

namespace NovoForecastingSystem.ViewModels
{
    public class DashBoardViewModel : BaseViewModel
    {
        private string _projectName = string.Empty;
        public string ProjectName
        {
            get => _projectName;
            set { _projectName = value; OnPropertyChanged(); }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); }
        }

        private string? _complexity;
        public string? Complexity
        {
            get => _complexity;
            set { _complexity = value; OnPropertyChanged(); }
        }

        public ICommand NavigateToProject { get; }
        public ICommand CreateProjectCommand { get; }

        private ProjectRepo projectRepo = new ProjectRepo();

        public ObservableCollection<Project> ProjectList { get; set; }

        public DashBoardViewModel(NavigationStore navigationStore)
        {
            ProjectList = new ObservableCollection<Project>(projectRepo.GetAllProjects());
            NavigateToProject = new NavigateCommand(new NavigationService(navigationStore, () => new ProjectViewModel(navigationStore)));
            CreateProjectCommand = new CreateProjectCommand();
        }

        public void CreateProject()
        {
            DateOnly endDate = DateOnly.MinValue;
            DateOnly startDate = StartDate.HasValue ? DateOnly.FromDateTime(StartDate.Value) : DateOnly.MinValue;
            try
            {
                if (string.IsNullOrWhiteSpace(ProjectName))
                {
                    MessageBox.Show("Please enter a project name.");
                    return;
                }

                if (StartDate.HasValue && !string.IsNullOrEmpty(Complexity))
                {
                    Enum.TryParse(Complexity, out ComplexityEnum complexityEnum);
                    Project temp = new Project { StartDate = startDate, ComplexityEnum = complexityEnum };
                    endDate = temp.EndDate;
                }

                ProjectRepo projectRepo = new ProjectRepo();
                Project project = projectRepo.CreateProject(ProjectName, Complexity, startDate, endDate);

                MessageBox.Show("Project successfully created in database!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
                ProjectList.Add(project);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
