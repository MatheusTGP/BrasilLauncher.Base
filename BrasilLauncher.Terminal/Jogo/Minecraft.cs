using System.Diagnostics;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using Spectre.Console;

internal class Minecraft
{
    private readonly MinecraftLauncher launcher = new();

    public async Task AbrirMinecraft(
        MSession session,
        string versao,
        bool baixar = true
    ) {
        Process? jogo;
        MLaunchOption opcoes = new()
        {
            Session = session,
            MaximumRamMb = 4096
        };
        
        if (baixar) {
            AnsiConsole.MarkupLine($"[yellow]Baixando minecraft [/][white]{versao}[/]");
            jogo = await launcher.InstallAndBuildProcessAsync(versao, opcoes);
        } else {
            AnsiConsole.MarkupLine($"[yellow]Construindo processo da [/][white]{versao}[/]");
            jogo = await launcher.BuildProcessAsync(versao, opcoes);
        }

        AnsiConsole.Status()
            .Start($"Iniciando minecraft {versao}", ctx => {
                Thread.Sleep(2000);
                jogo.Start();
            });
    }

    public async Task<string> PerguntarVersao()
    {
        var versoes = await launcher.GetAllVersionsAsync();
        var selecao = new SelectionPrompt<string>().Title("[white bold]Selecione a versão[/]");

        foreach (var versao in versoes)
        {
            if (versao.Type == "release")
            {
                selecao.AddChoice(versao.Name);
            }
        }

        return AnsiConsole.Prompt(selecao);
    }

    
}