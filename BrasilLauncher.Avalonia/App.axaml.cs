using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BrasilLauncher.Navigation;
using BrasilLauncher.Services;
using BrasilLauncher.Utils;
using BrasilLauncher.ViewModels;
using BrasilLauncher.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BrasilLauncher {
    public partial class App : Application {
        public override void Initialize() {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted() {
            if (Design.IsDesignMode) {
                base.OnFrameworkInitializationCompleted();
                return;
            }

            var services = new ServiceCollection();
            services.AddSingleton<MicrosoftService>();
            services.AddSingleton<ProfileService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton(provider => new ProfileViewModel(provider.GetRequiredService<ProfileService>()));
            services.AddSingleton<HomeViewModel>();

            var provider = services.BuildServiceProvider();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                desktop.MainWindow = new MainWindow {
                    DataContext = provider.GetRequiredService<MainWindowViewModel>()
                };
            }

            // Cria os diretorios necessários para o funcionamento do launcher
            Paths.EnsureDirectories();
            base.OnFrameworkInitializationCompleted();
        }
    }
}