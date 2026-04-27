using NovoForecastingSystem.ViewModels;
using System;
using System.Windows.Input;

namespace NovoForecastingSystem.Commands
{
    public class CreateProjectCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is DashBoardViewModel dvm)
                dvm.CreateProject();
        }
    }
}