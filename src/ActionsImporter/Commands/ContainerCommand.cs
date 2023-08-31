using System.Collections.Immutable;
using System.CommandLine;
using ActionsImporter.Handlers;

namespace ActionsImporter.Commands;

public abstract class ContainerCommand : BaseCommand
{
    private readonly string[] _args;

    protected ContainerCommand(string[] args)
    {
        // Don't forward the --prerelease or --no-host-network flag to GitHub Actions Importer image
        _args = args.Where(arg => !arg.Contains(Common.Prerelease.Name, StringComparison.Ordinal) && !arg.Contains(Common.NoHostNetwork.Name, StringComparison.Ordinal)).ToArray();
    }

    protected abstract ImmutableArray<Option> Options { get; }

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);

        foreach (var option in Options)
        {
            command.AddOption(option);
        }

        command.SetHandler(new ContainerHandler(app).Run(_args));

        return command;
    }
}
