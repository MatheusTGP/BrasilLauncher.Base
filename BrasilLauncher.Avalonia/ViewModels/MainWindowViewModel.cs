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

        [ObservableProperty]
        private MSession profile = MSession.CreateOfflineSession("Steve");

        // ID do cliente, usado para autenticação online no Azure
        private string clientId = "INSERT_CLIENT_ID_HERE";

        public ObservableCollection<string> Versions { get; set; } = [];

        [ObservableProperty]
        private string? selectedVersion;

        [ObservableProperty]
        private bool playOffline;

        public MainWindowViewModel() {
            _ = LoadVersions();
        }

        public async Task LoadVersions() {
            var versions = await Minecraft.GetAllVersionsAsync();
            Versions.Clear();

            foreach (var version in versions) {
                Versions.Add(version.Name);
            }

            SelectedVersion = Versions.FirstOrDefault();
        }

        [RelayCommand]
        public async Task LaunchGame() {
            if (SelectedVersion == null) return;
            var option = new MLaunchOption { Session = Profile };
            var process = await Minecraft.InstallAndBuildProcessAsync(SelectedVersion, option);
            process.Start();
        }

        [RelayCommand]
        public async Task LoginMicrosoft() {
            var app = await MsalClientHelper.BuildApplicationWithCache(clientId);
            var loginHandler = new JELoginHandlerBuilder()
                .WithOAuthProvider(new MsalCodeFlowProvider(app))
                .Build();

            try {
                Profile = await loginHandler.AuthenticateSilently(); 
            } catch {
                Profile = await loginHandler.AuthenticateInteractively();
            }
        }

        [RelayCommand]
        public void TogglePlayOffline() {
            if (PlayOffline) {
                Profile = MSession.CreateOfflineSession("Steve");
                PlayOffline = true;
            } else {
                PlayOffline = false;
            }
        }

    }
}
