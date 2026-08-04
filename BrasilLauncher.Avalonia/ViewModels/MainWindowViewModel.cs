using Avalonia.Media.Imaging;
using BrasilLauncher.Navigation;
using BrasilLauncher.Services;
using BrasilLauncher.Utils;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;
using CmlLib.Core.ProcessBuilder;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XboxAuthNet.Game.Msal;
using XboxAuthNet.Game.Msal.OAuth;

namespace BrasilLauncher.ViewModels {
    public partial class MainWindowViewModel : ViewModelBase {

        private readonly MinecraftLauncher Minecraft = new();

        public INavigationService Navigation { get; }

        [ObservableProperty]
        private ProfileService profile;

        public ObservableCollection<string> Versions { get; set; } = [];

        [ObservableProperty]
        private string? selectedVersion;

        [ObservableProperty]
        private bool playOffline;

        public MainWindowViewModel(INavigationService navigation, ProfileService profileService) {
            Navigation = navigation;
            Profile = profileService;
            _ = LoadVersions();
        }

        [RelayCommand]
        public void NavigateToHome() {
            Navigation.Navigate<HomeViewModel>();
        }

        [RelayCommand]
        public void NavigateToProfile() {
            Navigation.Navigate<ProfileViewModel>();
        }

        public async Task LoadVersions() {
            var versions = await Minecraft.GetAllVersionsAsync();
            Versions.Clear();

            foreach (var version in versions) {
                if (version.Type == "release") {
                    Versions.Add(version.Name);
                }
            }

            SelectedVersion = Versions.FirstOrDefault();
        }

        [RelayCommand]
        public async Task LaunchGame() {
            if (SelectedVersion == null) return;
            var option = new MLaunchOption { Session = Profile.Session };
            var process = await Minecraft.InstallAndBuildProcessAsync(SelectedVersion, option);
            process.Start();
        }

        [RelayCommand]
        public void TogglePlayOffline() {
            // Pensar em outras formas de implementar o modo offline.
        }

    }
}
