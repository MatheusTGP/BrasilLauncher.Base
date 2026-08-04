using System;
using System.Collections.Generic;
using System.Text;

namespace BrasilLauncher.Util;

internal class BuildInfo {
    public static string LauncherName { get; set; } = "BrasilLauncher";
    public static string Version { get; set; } = "1.1-Avalonia";
    public static string AzureClientCode { get; set; } = "499c8d36-be2a-4231-9ebd-ef291b7bb64c"; // Do código de exemplo do Cmllib.Core
    public static string DefaultUsername { get; set; } = "Steve";
    public static string CmllibAccessToken { get; set; } = "access_token";
}