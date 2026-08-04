using Avalonia.Media.Imaging;
using BrasilLauncher.Services;
using BrasilLauncher.Util;
using BrasilLauncher.Utils;
using CmlLib.Core.Auth.Microsoft;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XboxAuthNet.Game.Msal;
using XboxAuthNet.Game.Msal.OAuth;

namespace BrasilLauncher.ViewModels {
    public partial class ProfileViewModel : ViewModelBase {
        [ObservableProperty]
        public ProfileService profile; 

        [ObservableProperty]
        public Bitmap? skinImage;

        public ProfileViewModel(ProfileService profileService) {
            profile = profileService;
            _ = ReloadSkin();
        }

        [RelayCommand]
        public async Task Login() {
            var result = await MicrosoftService.Login();
            Profile.SetSession(result);
            await ReloadSkin();
        }

        [RelayCommand]
        public async Task Logout() {
            Profile.SetOfflineSession(BuildInfo.DefaultUsername);
            await ReloadSkin();
        }

        [RelayCommand]
        public async Task ReloadSkin() {
            SkinImage = await AvatarService.GetAvatarAsync(Profile.Session.Username ?? BuildInfo.DefaultUsername, isReload: true);
        }
    }
}
