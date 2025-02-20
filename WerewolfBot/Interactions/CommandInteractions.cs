using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace WerewolfBot.Interactions;

public class CommandInteractions : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("create", "Creates a new Werewolf-Game in this channel and the voice channel you're currently in.")]
    public async Task CreateGame()
    {
        await InteractionResolver.CreateGame(Context);
    }

    [SlashCommand("abandone", "Abandones the current Werewolf-Game in this Server.")]
    public async Task Abandone()
    {
        await InteractionResolver.Abandone(Context);
    }
}