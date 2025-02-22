using Discord.Audio;
using Discord.Interactions;
using System.Diagnostics;

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
        // Elevenlabs -> Bill
        try
        {
            string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets", "output.mp3"));
            var smth = Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-hide_banner -loglevel panic -i \"{filePath}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            });

            using (var ffmpeg = smth)
            using (var output = ffmpeg.StandardOutput.BaseStream)
            using (var discord = WerewolfClient.currentGame.currentVoiceConnection.CreatePCMStream(AudioApplication.Mixed))
            {
                try { await output.CopyToAsync(discord); }
                finally { await discord.FlushAsync(); }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [ComponentInteraction("upload_current_game_settings")]
    public async Task HandleInfoButtonUpload()
    {
        await InteractionResolver.UploadSettingsPrompt(Context);
    }

    [ComponentInteraction("download_current_game_settings")]
    public async Task HandleInfoButtonDownload()
    {
        await InteractionResolver.DownloadSettings(Context);
    }
    
    [ComponentInteraction("join_current_game")]
    public async Task HandleJoinButton()
    {
        await InteractionResolver.JoinGame(Context);
    }

    [ComponentInteraction("leave_current_game")]
    public async Task HandleLeaveButton()
    {
        await InteractionResolver.LeaveGame(Context);
    }
}