using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CliFx;
using DiscordChatExporter.Cli.Commands;
using DiscordChatExporter.Core.Exporting.Filtering;
using DiscordChatExporter.Core.Exporting.Partitioning;

namespace DiscordChatExporter.Cli;

public static class Program
{
    // Explicit references because CliFx relies on reflection and we're publishing with trimming enabled
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ExportChannelsCommand))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PartitionLimit))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MessageFilter))]
    public static async Task<int> Main(string[] args) =>
        await new CliApplicationBuilder()
            .AddCommand<ExportChannelsCommand>()
            .Build()
            .RunAsync(args);
}
