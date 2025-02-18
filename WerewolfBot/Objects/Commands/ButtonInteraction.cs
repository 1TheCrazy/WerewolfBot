using Discord.Interactions;

namespace WerewolfBot.Objects.Commands;

/// <summary>
/// A class for handling all button interactions with the bot
/// </summary>
class ButtonInteraction: InteractionModuleBase<SocketInteractionContext>
{
    [ComponentInteraction("abandone_game")]
    public async Task HandleAbandoneButton()
    {
        if (WerewolfClient.currentGame is null)
            await RespondAsync($"There was no active Game to abandone in this server!");

        if (WerewolfClient.currentVoiceConnection is not null)
            await WerewolfClient.currentVoiceConnection.StopAsync();

        WerewolfClient.currentGame?.Abandone();

        await RespondAsync("✅ Abandoning the current Werewolf-Game!");
    }

    [ComponentInteraction("start_current_game")]
    public async Task HandleDislikeButton()
    {
        
    }

    [ComponentInteraction("edit_current_game_settings")]
    public async Task HandleInfoButton()
    {
        
    }
}