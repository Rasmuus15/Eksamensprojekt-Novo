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

        public ProjectViewModel(Project project, NavigationStore? navigationStore = null)
        {
            CurrentProject = project;
            NavigateToDashboardViewCommand = new NavigateCommand(new NavigationService(navigationStore, () => new DashBoardViewModel(navigationStore)));
            AddResourceCommand = new AddResourceCommand();
            EditProjectCommand = new EditProjectCommand();
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
