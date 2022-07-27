namespace DiscordBot;

public class Program
{
    public static Task Main() => new Program().MainAsync();

    private async Task MainAsync()
    {
        var discordBot = new DiscordBot();
        await discordBot.Initialize();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }
}