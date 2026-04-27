using NovoForecastingSystem.Commands;
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

        public ICommand NavigateToDashboardViewCommand { get; }
        public ICommand AddResourceCommand { get; }
        public ICommand EditProjectCommand { get; }
        public ICommand CreateProjectCommand { get; }

        public ProjectViewModel(NavigationStore? navigationStore = null)
        {
            if (navigationStore != null)
            {
                NavigateToDashboardViewCommand = new NavigateCommand(new NavigationService(navigationStore, () => new DashBoardViewModel(navigationStore)));
            }
            AddResourceCommand = new AddResourceCommand();
            EditProjectCommand = new EditProjectCommand();
            CreateProjectCommand = new CreateProjectCommand(ExecuteCreateProject);
        }

        private void ExecuteCreateProject(object? parameter)
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

                if (parameter is Window window)
                {
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        public void AddResource()
        {
            AddResourceWindow addResourceWindow = new AddResourceWindow();
            addResourceWindow.Show();
        }

        public void EditProject()
        {
            EditProjectWindow editProjectWindow = new EditProjectWindow();
            editProjectWindow.Show();
        }
    }
}
