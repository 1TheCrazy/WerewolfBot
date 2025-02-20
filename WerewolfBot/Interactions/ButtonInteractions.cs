using Discord.Interactions;

namespace WerewolfBot.Interactions;

/// <summary>
/// A class for handling all button interactions with the bot
/// </summary>
public class ButtonInteractions: InteractionModuleBase<SocketInteractionContext>
{
    [ComponentInteraction("abandone_game")]
    public async Task HandleAbandoneButton()
    {
        await InteractionResolver.Abandone(Context);
    }

    [ComponentInteraction("start_current_game")]
    public async Task HandleStartGame()
    {

    }

    [ComponentInteraction("upload_current_game_settings")]
    public async Task HandleInfoButtonUpload()
    {
        
    }

    [ComponentInteraction("download_current_game_settings")]
    public async Task HandleInfoButtonDownload()
    {

    }
}