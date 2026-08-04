using BrasilLauncher.Util;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XboxAuthNet.Game.Msal;
using XboxAuthNet.Game.Msal.OAuth;

namespace BrasilLauncher.Services;

public class MicrosoftService {
    public static async Task<MSession> Login() {  
        var app = await MsalClientHelper.BuildApplicationWithCache(BuildInfo.AzureClientCode);
        var loginHandler = new JELoginHandlerBuilder()
            .WithOAuthProvider(new MsalCodeFlowProvider(app))
            .Build();

        try {
            return await loginHandler.AuthenticateSilently();
        }
        catch (Exception) {
            return await loginHandler.AuthenticateInteractively();
        }
    }
}
