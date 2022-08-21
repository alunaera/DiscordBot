namespace DiscordBot;

using System.Collections.Concurrent;

public class GuildChannelConfig
{
    public ulong ChannelId;
    public IDictionary<string, ulong> GivingRolesByReaction;

    public GuildChannelConfig(ulong channelId)
    {
        this.ChannelId             = channelId;
        this.GivingRolesByReaction = new ConcurrentDictionary<string, ulong>();
    }
}