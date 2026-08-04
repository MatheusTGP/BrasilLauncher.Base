using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BrasilLauncher.Utils;
internal class Paths {
    public static string AppData { get; } =
       Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BrasilLauncher");

    public static string Cache { get; } =
        Path.Combine(AppData, "Cache");

    public static string AvatarCache { get; } =
        Path.Combine(Cache, "Skin.png");

    public static string Settings { get; } =
        Path.Combine(AppData, "settings.json");

    public static void EnsureDirectories() {
        Directory.CreateDirectory(AppData);
        Directory.CreateDirectory(Cache);
    }
}
