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

        public ObservableCollection<Project> ProjectList;

        public DashBoardViewModel(NavigationStore navigationStore)
        {
            projectRepo.GetAllProjects();
            NavigateToProject = new NavigateCommand(new NavigationService(navigationStore, () => new ProjectViewModel(navigationStore)));
            CreateProjectCommand = new CreateProjectCommand();
        }

        public void CreateProject()
        {
            try
            {
                DateTime? endDate = null;
                if (StartDate.HasValue && !string.IsNullOrEmpty(Complexity))
                {
                    if (Complexity == "Low") endDate = StartDate.Value.AddDays(81 * 7);
                    else if (Complexity == "Medium") endDate = StartDate.Value.AddDays(108 * 7);
                    else if (Complexity == "High") endDate = StartDate.Value.AddDays(137 * 7);
                }

                if (string.IsNullOrWhiteSpace(ProjectName))
                {
                    MessageBox.Show("Please enter a project name.");
                    return;
                }

                var repo = new ProjectRepo();
                repo.CreateProject(ProjectName, Complexity, StartDate, endDate);

                MessageBox.Show("Project successfully created in database!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
