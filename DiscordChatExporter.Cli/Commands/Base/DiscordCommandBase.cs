using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using DiscordChatExporter.Core.Discord;

namespace DiscordChatExporter.Cli.Commands.Base;

public abstract class DiscordCommandBase : ICommand
{
    [CommandOption(
        "token",
        't',
        EnvironmentVariable = "DISCORD_TOKEN",
        Description = "Authentication token."
    )]
    public required string Token { get; init; }

    private DiscordClient? _discordClient;
    protected DiscordClient Discord => _discordClient ??= new DiscordClient(Token);

    public virtual ValueTask ExecuteAsync(IConsole console)
    {
        return default;
    }
}