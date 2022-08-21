namespace DiscordBot;

using System.Collections.Concurrent;

public partial class DiscordBot
{
    private readonly DiscordSocketClient client;
    private readonly IDictionary<ulong, SocketGuild> guildInfos;
    private readonly IDictionary<ulong, GuildConfig> guildConfigs;
    private Config? config;

    public DiscordBot()
    {
        this.client       = new DiscordSocketClient();
        this.guildInfos   = new ConcurrentDictionary<ulong, SocketGuild>();
        this.guildConfigs = new ConcurrentDictionary<ulong, GuildConfig>();
    }

    public async Task Initialize()
    {
        try
        {
            string appSettings = await File.ReadAllTextAsync("appsettings.json").ConfigureAwait(false);
            this.config = JsonConvert.DeserializeObject<Config>(appSettings);
            
            // TODO Add load guild configs from database

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

    private GuildChannelConfig? GetGuildChannelConfig(ulong guildId, ulong channelId)
    {
        this.guildConfigs.TryGetValue(guildId, out GuildConfig? guildConfig);

        GuildChannelConfig? guildChannelConfig = null;
        guildConfig?.GuildChannelConfigs.TryGetValue(channelId, out guildChannelConfig);

        return guildChannelConfig;
    }
}