using Discord.Interactions;

public class SlashCommandModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Replies with Pong!")]
    public async Task Ping()
    {
        await RespondAsync("Pong!");
    }
}