using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Text;
using WerewolfBot.Objects;

namespace WerewolfBot.Interactions;
class InteractionResolver
{
    public static async Task Abandone(SocketInteractionContext ctx)
    {
        if (WerewolfClient.currentGame is null)
        {
            await ctx.Interaction.RespondAsync($"There was no active Game to abandone in this server!", ephemeral: true);
            return;
        }   

        WerewolfClient.currentGame.Abandone(ctx.User);
        WerewolfClient.currentGame = null;

        await ctx.Interaction.RespondAsync("❌ Abandoning the current Werewolf-Game!");
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

        if (WerewolfClient.currentGame is not null)
        {
            await ctx.Interaction.RespondAsync("❌ There is already a game running!");
            return;
        }

        var buttons = new ComponentBuilder()
            .WithButton(label: "🔗 Join", customId: "join_current_game", row: 0, style: ButtonStyle.Success)
            .WithButton(label: "🔗 Leave", customId: "leave_current_game", row: 0, style: ButtonStyle.Danger)
            .WithButton(label: "Start", customId: "start_current_game", row: 1, style: ButtonStyle.Success)
            .WithButton(label: "Upload Settings ⚙️", customId: "upload_current_game_settings", row: 2)
            .WithButton(label: "Download Settings ⚙️", customId: "download_current_game_settings", row: 2)
            .WithButton(label: "Abandone", customId: "abandone_game", row: 3, style: ButtonStyle.Danger);

        WerewolfClient.currentGame = new();

        // Send a message in the text channel where the command was used
        await ctx.Interaction.RespondAsync($"✅ Creating a new Werewolf-Game in Voice-Channel: **{voiceChannel.Name}** and Text-Channel: **{textChannel.Name}**!\n", components: buttons.Build());

        // Connect to the user's voice channel
        WerewolfClient.currentGame.currentVoiceConnection = await voiceChannel.ConnectAsync();

        // Explicitly keep audio alive, since Discord disconnects the bot after ~15s of sedning no packages
        //await WerewolfClient.currentGame.KeepAudioAlive();

        // Return to loose scope
        return;
    }

    public static async Task UploadSettingsPrompt(SocketInteractionContext ctx)
    {
        // Safety checks
        if(WerewolfClient.currentGame is null)
        {
            await ctx.Interaction.RespondAsync($"❌ There is no ongoing game to attach settings to!", ephemeral: true);
            return;
        }
        if (WerewolfClient.currentGame.isRunning)
        {
            await ctx.Interaction.RespondAsync($"❌ The current game is running, therefore the settings cannot be changed!", ephemeral: true);
            return;
        }

        // Send a message in the text channel where the command was used
        await ctx.Interaction.RespondAsync($"✅ Please attach a .json file containing the settings to your next message!", ephemeral: true);


        // Listen to next message. Do it all inline to keep structure of Interaction Resolver clean.
        var tcs = new TaskCompletionSource<SocketMessage>();

        Task MessageHandler(SocketMessage message)
        {
            if (message.Author.Id == ctx.User.Id && message.Attachments.Count > 0)
            {
                // Save attachments here
                ctx.Client.MessageReceived -= MessageHandler; // Unsubscribe lambda
                tcs.SetResult(message);
            }
            return Task.CompletedTask;
        }

        ctx.Client.MessageReceived += MessageHandler;

        // Wait for 20s for the user to upload settings
        var delayTask = Task.Delay(TimeSpan.FromSeconds(20));
        var completedTask = await Task.WhenAny(tcs.Task, delayTask);

        ctx.Client.MessageReceived -= MessageHandler; // Ensure cleanup

        var result = completedTask == tcs.Task ? tcs.Task.Result : null;

        // Check if user uploaded in time
        if(result is null || !result.Attachments.Any())
        {
            await ctx.Interaction.FollowupAsync($"❌ You did not upload a .json containing game settings in time.", ephemeral: true);
            return;
        }

        // Check for conversion erros before notifying everyone
        var attachment = result.Attachments.First();

        if (!attachment.Filename.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
        {
            await ctx.Interaction.FollowupAsync($"❌ You did not upload a valid JSON.", ephemeral: true);
            return;
        }

        // Get content and convert
        try
        {
            string json = "";
            using (HttpClient httpClient = new())
            {
                json = await httpClient.GetStringAsync(attachment.Url);
            }
            Console.WriteLine(json);
            GameSettings settings = JsonConvert.DeserializeObject<GameSettings>(json)??throw new Exception("Conversion returned null.");

            WerewolfClient.currentGame.settings = settings;
        }
        catch
        {
            await ctx.Interaction.FollowupAsync($"❌ JSON was invalid.", ephemeral: true);
            return;
        }

        // Delete the message to keep chat clean
        await result.DeleteAsync();

        await ctx.Interaction.FollowupAsync($"✅ **{ctx.User.Username}** uploaded some settings that will be used!");
    }

    public static async Task UploadSettingsCmd(SocketInteractionContext ctx, IAttachment file)
    {
        // Safety checks
        if (WerewolfClient.currentGame is null)
        {
            await ctx.Interaction.RespondAsync($"❌ There is no ongoing game to attach settings to!", ephemeral: true);
            return;
        }
        if (WerewolfClient.currentGame.isRunning)
        {
            await ctx.Interaction.RespondAsync($"❌ The current game is running, therefore the settings cannot be changed!", ephemeral: true);
            return;
        }

        if (!file.Filename.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
        {
            await ctx.Interaction.RespondAsync($"❌ You did not upload a valid JSON.", ephemeral: true);
            return;
        }

        // Get content and convert
        try
        {
            string json = "";
            using (HttpClient httpClient = new())
            {
                json = await httpClient.GetStringAsync(file.Url);
            }
            Console.WriteLine(json);
            GameSettings settings = JsonConvert.DeserializeObject<GameSettings>(json) ?? throw new Exception("Conversion returned null.");

            WerewolfClient.currentGame.settings = settings;
        }
        catch
        {
            await ctx.Interaction.RespondAsync($"❌ JSON was invalid.", ephemeral: true);
            return;
        }

        await ctx.Interaction.RespondAsync($"✅ **{ctx.User.Username}** uploaded some settings that will be used!");
    }

    public static async Task DownloadSettings(SocketInteractionContext ctx)
    {
        if(WerewolfClient.currentGame is null)
        {
            await ctx.Interaction.RespondAsync("❌ There is no game running to download settings from.", ephemeral: true);
            return;
        }

        string json = JsonConvert.SerializeObject(WerewolfClient.currentGame.settings);

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        await ctx.Interaction.RespondWithFileAsync(stream, "werewolf_settings.json", "✅ Here are the settings used in the current game:", ephemeral: true);
    }

    public static async Task JoinGame(SocketInteractionContext ctx)
    {
        if (WerewolfClient.currentGame is null)
        {
            await ctx.Interaction.RespondAsync("❌ There is no game running to join.", ephemeral: true);
            return;
        }

        IGuildUser? user = ctx.User as IGuildUser;

        if(user is null)
        {
            await ctx.Interaction.RespondAsync("❌ Failed to join game...", ephemeral: true);
            return;
        }

        Player playerToAdd = new Player(user);

        WerewolfClient.currentGame.players.Add(playerToAdd);

        await ctx.Interaction.RespondAsync($"✅ **{ctx.User.Username}** joined the game!");
    }

    public static async Task LeaveGame(SocketInteractionContext ctx)
    {
        if (WerewolfClient.currentGame is null)
        {
            await ctx.Interaction.RespondAsync("❌ There is no game running to leave.", ephemeral: true);
            return;
        }

        IGuildUser? user = ctx.User as IGuildUser;

        if (user is null)
        {
            await ctx.Interaction.RespondAsync("❌ Failed to leave game...", ephemeral: true);
            return;
        }

        // Remove player from the current running game
        WerewolfClient.currentGame.players.Remove(WerewolfClient.currentGame.players.Where(p => p.discordUser == user).First());

        await ctx.Interaction.RespondAsync($"❌ **{ctx.User}** left the game!");
    }
}