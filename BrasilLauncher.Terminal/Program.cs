using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using CmlLib.Core.Rules;
using Spectre.Console;

class Program
{
    public static async Task Main()
    {
        MSession sessao;
        var minecraft = new Minecraft();
        var minecraftAuth = new MinecraftAuth();

        AnsiConsole.MarkupLine("[green]Bem-vindo ao [/][white]Brasil Launcher![/]");

        var fazerLogin = Menus.Confirmar("Deseja logar na Microsoft? ");
        // Logar na microsoft
        if (fazerLogin)
        {
            sessao = await minecraftAuth.FazerLogin();
        } else
        {
            // Logar Offline
            var nome = Menus.Perguntar("Qual o nome do [green]perfil[/]: ");
            sessao = MSession.CreateOfflineSession(nome);
        }
        AnsiConsole.MarkupLine($"[white]Você está logado como[/] [green]{sessao.Username}[/]");

        var versao = await minecraft.PerguntarVersao();
        var baixar = Menus.Confirmar("Deseja baixar a versão se não tiver baixada?");
        await minecraft.AbrirMinecraft(sessao, versao, baixar);
    }
}