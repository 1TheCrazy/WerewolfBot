using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace WerewolfBot.Objects.Commands;
class InteractionResolver
{
    public static async Task Abandone(SocketInteractionContext ctx)
    {
        if (WerewolfClient.currentGame is null)
            await ctx.Interaction.RespondAsync($"There was no active Game to abandone in this server!");

        if (WerewolfClient.currentVoiceConnection is not null)
            await WerewolfClient.currentVoiceConnection.StopAsync();

        WerewolfClient.currentGame?.Abandone();

        await ctx.Interaction.RespondAsync("✅ Abandoning the current Werewolf-Game!");
    }

    public static async Task CreateGame(SocketInteractionContext ctx)
    {
        var user = ctx.User as SocketGuildUser;
        var voiceChannel = user?.VoiceChannel;
        var textChannel = ctx.Channel; // The channel where the command was executed

        // If the User is not connected to any voice 
        if (voiceChannel == null)
        {
            await ctx.Interaction.RespondAsync("❌ You need to be in a voice channel to create a game!");
            return;
        }

        if (WerewolfClient.currentGame is not null || WerewolfClient.currentVoiceConnection is not null)
        {
            await ctx.Interaction.RespondAsync("❌ There is already a game running!");
            return;
        }

        // Connect to the user's voice channel
        WerewolfClient.currentVoiceConnection = await voiceChannel.ConnectAsync();

        var buttons = new ComponentBuilder()
            .WithButton(label: "Start", customId: "start_current_game", row: 0, style: ButtonStyle.Success)
            .WithButton(label: "Upload Settings ⚙️", customId: "upload_current_game_settings", row: 1)
            .WithButton(label: "Download Settings ⚙️", customId: "download_current_game_settings", row: 1)
            .WithButton(label: "Abandone", customId: "abandone_game", row: 2, style: ButtonStyle.Danger);

        // Send a message in the text channel where the command was used
        await ctx.Interaction.RespondAsync($"✅ Creating a new Werewolf-Game in Voice-Channel: **{voiceChannel.Name}** and Text-Channel: **{textChannel.Name}**!\n", components: buttons.Build());
    }
}