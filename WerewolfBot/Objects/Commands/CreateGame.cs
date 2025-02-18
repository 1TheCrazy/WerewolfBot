using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace WerewolfBot.Objects.Commands;

public class CreateGameCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("create", "Creates a new Werewolf-Game in this channel and the voice channel you're currently in.")]
    public async Task Create()
    {
        var user = Context.User as SocketGuildUser;
        var voiceChannel = user?.VoiceChannel;
        var textChannel = Context.Channel; // The channel where the command was executed

        // If the User is not connected to any voice 
        if (voiceChannel == null)
        {
            await RespondAsync("❌ You need to be in a voice channel to create a game!");
            return;
        }

        if(WerewolfClient.currentGame is not null || WerewolfClient.currentVoiceConnection is not null)
        {
            await RespondAsync("❌ There is already a game running!");
            return;
        }

        // Connect to the user's voice channel
        WerewolfClient.currentVoiceConnection = await voiceChannel.ConnectAsync();

        var buttons = new ComponentBuilder()
            .WithButton(label: "Start", customId: "start_current_game", row: 0, style: ButtonStyle.Success)
            .WithButton(label: "Settings ⚙️", customId: "edit_current_game_settings", row: 1)
            .WithButton(label: "Abandone", customId: "abandone_game", row: 2, style: ButtonStyle.Danger);

        // Send a message in the text channel where the command was used
        await RespondAsync($"✅ Creating a new Werewolf-Game in Voice-Channel: **{voiceChannel.Name}** and Text-Channel: **{textChannel.Name}**!\n", components: buttons.Build());
    }
}