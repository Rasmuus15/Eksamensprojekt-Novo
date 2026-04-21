using NovoForecastingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NovoForecastingSystem.Commands
{
    public class AddResourceCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ProjectViewModel pvm)
                pvm.AddResource();
        }
    }
}
