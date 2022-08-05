namespace DiscordBot;

using System.Collections.Concurrent;

public class GuildChannelConfig
{
    public ulong ChannelId;
    public IDictionary<IEmote, SocketRole> GivingRolesByReaction;

    public GuildChannelConfig(ulong channelId)
    {
        this.ChannelId             = channelId;
        this.GivingRolesByReaction = new ConcurrentDictionary<IEmote, SocketRole>();
    }
}