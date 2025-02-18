using Discord.Interactions;

namespace WerewolfBot.Objects.Commands;

public class PingPongCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Replies with Pong!")]
    public async Task Ping()
    {
        await RespondAsync("Pong!");
    }
}