using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using NovoForecastingSystem.Stores;
using NovoForecastingSystem.ViewModels;
using NovoForecastingSystem.Services;

namespace NovoForecastingSystem.Commands
{
    public class NavigateCommand : ICommand
    {
        private readonly NavigationService _navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
