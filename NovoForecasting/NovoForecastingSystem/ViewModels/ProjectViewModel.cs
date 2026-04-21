using NovoForecastingSystem.Commands;
using NovoForecastingSystem.Repos;
using NovoForecastingSystem.Services;
using NovoForecastingSystem.Stores;
using NovoForecastingSystem.Views;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Input;

namespace NovoForecastingSystem.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        public ICommand NavigateToDashboardViewCommand { get; }
        public ICommand AddResourceCommand { get; }
        public ProjectViewModel(NavigationStore navigationStore)
        {
            NavigateToDashboardViewCommand = new NavigateCommand(new NavigationService(navigationStore, () => new DashBoardViewModel(navigationStore)));
            AddResourceCommand = new AddResourceCommand();
        }

        public void AddResource()
        {
            AddResourceWindow addResourceWindow = new AddResourceWindow();
            addResourceWindow.Show();
            //ResourceRepo
            
        }
    }
}
