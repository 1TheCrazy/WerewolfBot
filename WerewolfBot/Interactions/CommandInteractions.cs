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

    [SlashCommand("downloadsettings", "Dowanlods the settings of the current game in a JSON format.")]
    public async Task DownloadSettings()
    {
        await InteractionResolver.DownloadSettings(Context);
    }

    [SlashCommand("uploadsettings", "Uploads settings in a JSON format to be used in the current game.")]
    public async Task UploadSettings([Summary("json", "Upload a JSON file containing the settings to be used.")] IAttachment file)
    {
        await InteractionResolver.UploadSettingsCmd(Context, file);
    }
}