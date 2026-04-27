using System;
using System.Windows.Input;

namespace NovoForecastingSystem.Commands
{
    public class CreateProjectCommand : ICommand
    {
        private readonly Action<object?> _execute;

        public CreateProjectCommand(Action<object?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => _execute(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}