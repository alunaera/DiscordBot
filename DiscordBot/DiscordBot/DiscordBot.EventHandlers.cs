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

    private Task OnReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
    {
        return Task.CompletedTask;
    }
}