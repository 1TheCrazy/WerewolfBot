using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace WerewolfBot;

class WerewolfClient
{
    private DiscordSocketClient client;
    private IServiceProvider services;
    InteractionService commands;

    private string token;

    public WerewolfClient(string botToken)
    {
        token = botToken;
        
        client = new();
        commands = new InteractionService(client);
        services = new ServiceCollection().BuildServiceProvider();

        
        client.Log += Log;
        client.Ready += RegisterCommandsAsync;
        client.InteractionCreated += HandleInteractionAsync;
    }

    public async Task RunBot()
    {
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();


        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }

    private async Task RegisterCommandsAsync()
    {
        // Finds Modules in all Files and loads them here
        await commands.AddModulesAsync(typeof(SlashCommandModule).Assembly, services);
        await commands.RegisterCommandsGloballyAsync();
    }

    private async Task HandleInteractionAsync(SocketInteraction interaction)
    {
        var ctx = new SocketInteractionContext(client, interaction);
        await commands.ExecuteCommandAsync(ctx, services);
    }
}