using BrasilLauncher.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrasilLauncher.Navigation;

public interface INavigationService {
    ViewModelBase? CurrentView { get; }
    void Navigate<T>() where T : ViewModelBase;
    void Navigate(Page page);
}
