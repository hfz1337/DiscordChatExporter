using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using DiscordChatExporter.Cli.Commands.Base;
using DiscordChatExporter.Core.Discord;

namespace DiscordChatExporter.Cli.Commands;

[Command("export", Description = "Exports one or multiple channels.")]
public class ExportChannelsCommand : ExportCommandBase
{
    [CommandOption(
        "file",
        'f',
        Description = "File containing the channel IDs (one per line)."
    )]
    public required string? ChannelsFile { get; init; }

    private List<Snowflake> ChannelIds { get; } = new();

    public override async ValueTask ExecuteAsync(IConsole console)
    {
        if (!string.IsNullOrEmpty(ChannelsFile))
        {
            if (!File.Exists(ChannelsFile))
            {
                throw new CommandException($"Channels file '{ChannelsFile}' does not exist.");
            }

            var lines = await File.ReadAllLinesAsync(ChannelsFile);
            ChannelIds.AddRange(lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(Snowflake.Parse));
        }

        await ExportAsync(console, ChannelIds);
    }
}