using Discord;
using Discord.Audio;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace WerewolfBot;

class WerewolfClient
{
    private DiscordSocketClient client;
    private IServiceProvider services;
    private InteractionService commands;

    public static Objects.Game? currentGame;

    public static IAudioClient? currentVoiceConnection;

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
        await commands.AddModulesAsync(typeof(Program).Assembly, services);
        //await commands.RegisterCommandsGloballyAsync();
        //await UnregisterGlobalCommands();

        // For faster testing
        await UnregisterGuildCommands();
        await commands.RegisterCommandsToGuildAsync(1341472914624483460);
    }

    private async Task HandleInteractionAsync(SocketInteraction interaction)
    {
        var ctx = new SocketInteractionContext(client, interaction);
        await commands.ExecuteCommandAsync(ctx, services);
    }

    private async Task UnregisterGuildCommands()
    {
        var guild = client.GetGuild(1341472914624483460);
        if (guild == null)
            return;

        var restGuild = await client.Rest.GetGuildAsync(1341472914624483460); // Get the guild via REST API
        var commands = await restGuild.GetApplicationCommandsAsync(); // Fetch existing commands

        foreach (var cmd in commands)
            await cmd.DeleteAsync();   
    }

    private async Task UnregisterGlobalCommands()
    {
        var commands = await client.Rest.GetGlobalApplicationCommands(); // Fetch all global commands

        foreach (var cmd in commands)
            await cmd.DeleteAsync();
        
    }

}