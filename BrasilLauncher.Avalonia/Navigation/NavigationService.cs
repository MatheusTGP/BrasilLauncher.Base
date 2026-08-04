using BrasilLauncher.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrasilLauncher.Navigation;

internal class NavigationService(IServiceProvider services) : ObservableObject, INavigationService {
    private readonly IServiceProvider _services = services;

    public ViewModelBase? _currentView = services.GetRequiredService<HomeViewModel>();
    public ViewModelBase? CurrentView {
        get => _currentView;
        private set => SetProperty(ref _currentView, value);
    }

    public void Navigate<T>() where T : ViewModelBase {
        var viewModel = _services.GetRequiredService<T>();
        if (CurrentView == viewModel) return;
        CurrentView = viewModel;
    }

    public void Navigate(Page page) {
        CurrentView = page switch {
            Page.Profile => _services.GetRequiredService<ProfileViewModel>(),
            _ => throw new ArgumentOutOfRangeException(nameof(page), page, null)
        };
    }
}