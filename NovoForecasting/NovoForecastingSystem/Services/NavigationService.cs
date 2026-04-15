using NovoForecastingSystem.Stores;
using NovoForecastingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovoForecastingSystem.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<BaseViewModel> _viewModelFactor;

        public NavigationService(NavigationStore navigationStore, Func<BaseViewModel> viewModelFactor)
        {
            _navigationStore = navigationStore;
            _viewModelFactor = viewModelFactor;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _viewModelFactor();
        }
    }
}
