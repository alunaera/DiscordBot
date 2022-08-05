namespace DiscordBot;

public class GuildConfig
{
    public ulong GuildId;
    public IDictionary<ulong, GuildChannelConfig> GuildChannelConfigs;

    public GuildConfig(ulong guildId)
    {
        this.GuildId             = guildId;
        this.GuildChannelConfigs = new Dictionary<ulong, GuildChannelConfig>();
    }
}