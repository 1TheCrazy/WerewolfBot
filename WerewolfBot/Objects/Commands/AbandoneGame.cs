using Discord.Interactions;

namespace WerewolfBot.Objects.Commands;

public class AbandoneGameCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("abandone", "Abandones the current Werewolf-Game in this Server.")]
    public async Task Abandone()
    {
        if(WerewolfClient.currentGame is null)
            await RespondAsync($"There was no active Game to abandone in this server!");

        if(WerewolfClient.currentVoiceConnection is not null)
            await WerewolfClient.currentVoiceConnection.StopAsync();

        WerewolfClient.currentGame?.Abandone();

        await RespondAsync("✅ Abandoning the current Werewolf-Game!");
    }
}