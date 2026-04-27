using NovoForecastingSystem.ViewModels;
using System;
using System.Windows.Input;

namespace NovoForecastingSystem.Commands
{
    public class CreateProjectCommand : ICommand
    {


      

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is ProjectViewModel pvm)
                pvm.CreateProject(null);
        }
        public event EventHandler? CanExecuteChanged;
    }
}