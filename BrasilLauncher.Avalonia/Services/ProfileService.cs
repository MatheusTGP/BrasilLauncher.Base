using BrasilLauncher.Util;
using CmlLib.Core.Auth;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrasilLauncher.Services;

public partial class ProfileService : ObservableObject {
    private MSession _session = MSession.CreateOfflineSession(BuildInfo.DefaultUsername);

    [ObservableProperty]
    public SessionType sessionType = SessionType.Local;

    [ObservableProperty]
    public bool isLogged = false;

    public MSession Session {
        get => _session;
        private set {
            if (SetProperty(ref _session, value)) {
                SessionType = value.AccessToken != BuildInfo.CmllibAccessToken
                    ? SessionType.Microsoft
                    : SessionType.Local;
                IsLogged = value.AccessToken != BuildInfo.CmllibAccessToken;
            }
        }
    }

    public void SetOfflineSession(string username) {
        Session = MSession.CreateOfflineSession(username);
    }

    public void SetSession(MSession newSession) {
        Session = newSession;
    }
}

public enum SessionType {
    Local,
    Microsoft
}