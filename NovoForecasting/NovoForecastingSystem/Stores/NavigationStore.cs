using System;
using System.Collections.Generic;
using System.Text;
using NovoForecastingSystem.ViewModels;

namespace NovoForecastingSystem.Stores
{
    public class NavigationStore
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnCurrentViewModelChanged(); }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
