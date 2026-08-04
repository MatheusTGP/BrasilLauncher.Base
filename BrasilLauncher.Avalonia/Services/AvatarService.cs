using Avalonia.Media.Imaging;
using BrasilLauncher.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BrasilLauncher.Services;

public class AvatarService {
    public static async Task<Bitmap> GetAvatarAsync(string username, bool isReload = false) {
        if (!File.Exists(Paths.AvatarCache) || isReload) {
            using var http = new HttpClient();

            byte[] bytes = await http.GetByteArrayAsync(
                $"https://api.mcheads.org/head/{username}.png");

            await File.WriteAllBytesAsync(Paths.AvatarCache, bytes);
        }
        return new Bitmap(Paths.AvatarCache);
    }
}
