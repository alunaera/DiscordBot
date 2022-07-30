namespace DiscordBot;

using System.Collections.Concurrent;

public partial class DiscordBot
{
    private readonly DiscordSocketClient client;
    private readonly IDictionary<ulong, SocketGuild> guildInfos;
    private Config? config;

    public DiscordBot()
    {
        this.client     = new DiscordSocketClient();
        this.guildInfos = new ConcurrentDictionary<ulong, SocketGuild>();
    }

    public async Task Initialize()
    {
        try
        {
            string appSettings = await File.ReadAllTextAsync("appsettings.json").ConfigureAwait(false);
            this.config = JsonConvert.DeserializeObject<Config>(appSettings);

            this.client.Log             += this.OnLog;
            this.client.GuildAvailable  += this.OnGuildAvailable;
            this.client.MessageReceived += this.OnMessageReceived;
            this.client.ReactionAdded   += this.OnReactionAdded;

            await this.client.LoginAsync(TokenType.Bot, this.config?.Token).ConfigureAwait(false);
            await this.client.StartAsync().ConfigureAwait(false);;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Initialize error!");
        }
    }
}