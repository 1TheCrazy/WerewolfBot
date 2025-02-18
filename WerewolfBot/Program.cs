// Invite Link for the Bot
// https://discord.com/oauth2/authorize?client_id=1341465385911844875&permissions=621217698021184&integration_type=0&scope=bot

// Main Entry Point for the Program
using WerewolfBot;

string token = Environment.GetEnvironmentVariable("WEREWOLF_BOT_TOKEN") ?? string.Empty;

if(token == string.Empty)
{
    Console.WriteLine("The Environment Variable for 'WEREWOLF_BOT_TOKEN' wasn't set, but it should contain the token of the WerewolfBot.");
    return;
}

WerewolfClient client = new(token);

await client.RunBot();