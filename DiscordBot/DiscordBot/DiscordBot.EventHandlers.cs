namespace DiscordBot;

public partial class DiscordBot
{
    private Task OnLog(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
    
    private Task OnMessageReceived(SocketMessage message)
    {
        if (message.Author.IsBot)
            return Task.CompletedTask;

        message.Channel.SendMessageAsync($"Hey, {message.Author.Mention}!").ConfigureAwait(false);
        return Task.CompletedTask;
    }

    private Task OnGuildAvailable(SocketGuild guild)
    {
        this.guildInfos[guild.Id] = guild;

        return Task.CompletedTask;
    }

    private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
    {
        try
        {
            var socketGuildChannel = message.Value.Channel as SocketGuildChannel;
            ulong? guildId = socketGuildChannel?.Guild.Id;

            if (guildId == null)
                return;

            GuildChannelConfig? guildChannelConfig = this.GetGuildChannelConfig(guildId.Value, channel.Id);
            SocketRole? socketRole = guildChannelConfig?.GivingRolesByReaction[reaction.Emote];

            if (socketRole == null)
                return;
        
            SocketGuild guildInfo = this.guildInfos[guildId.Value];
            SocketGuildUser? socketGuildUser = guildInfo.Users.FirstOrDefault(user => user.Id == reaction.User.Value.Id);

            if (socketGuildUser != null && !socketGuildUser.Roles.Contains(socketRole))
                await socketGuildUser.AddRoleAsync(socketRole.Id).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}