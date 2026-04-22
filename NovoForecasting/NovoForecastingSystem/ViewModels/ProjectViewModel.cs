using NovoForecastingSystem.Commands;
using NovoForecastingSystem.Services;
using NovoForecastingSystem.Stores;
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
        public ProjectViewModel(NavigationStore navigationStore)
        {
            NavigateToDashboardViewCommand = new NavigateCommand(new NavigationService(navigationStore, () => new DashBoardViewModel(navigationStore)));
        }
    }
}
