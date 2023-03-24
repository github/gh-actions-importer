using System.CommandLine.Parsing;
using ActionsImporter.Commands;

namespace ActionsImporter.Models;

public static class ParserExtensions
{
    public static async Task<int> InvokeAsync(this Parser? parser, App? app, string[] args, bool experimental)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(app);

        if (experimental)
        {
            Console.WriteLine("Experimental features are enabled. Use at your own risk.");

            await app.ExecuteActionsImporterAsync(args.Where(arg => !arg.Contains(Common.Experimental.Name, StringComparison.Ordinal)).ToArray());
            return 0;
        }

        return await parser.InvokeAsync(args);
    }
}
