using NovoForecastingSystem.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NovoForecastingSystem.ViewModels
{
    public class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel { get => _navigationStore.CurrentViewModel; }

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
