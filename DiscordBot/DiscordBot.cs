namespace DiscordBot;

public class DiscordBot
{
    private readonly DiscordSocketClient client;
    private Config? config;

    public DiscordBot()
    {
        this.client = new DiscordSocketClient();
    }

    public async Task Initialize()
    {
        string appSettings = await File.ReadAllTextAsync("appsettings.json").ConfigureAwait(false);
        this.config = JsonConvert.DeserializeObject<Config>(appSettings);

        this.client.Log += this.Log;
        this.client.MessageReceived += this.OnMessageReceived;

        await this.client.LoginAsync(TokenType.Bot, this.config?.Token);
        await this.client.StartAsync();
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private Task OnMessageReceived(SocketMessage message)
    {
        if (message.Author.IsBot)
            return Task.CompletedTask;

        message.Channel.SendMessageAsync($"Пока что я умею только отвечать, дорогой {message.Author.Mention}");
        return Task.CompletedTask;
    }
}