using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace ActionsImporter.Commands;

public class Version : BaseCommand
{
    protected override string Name => "version";
    protected override string Description => "Display the current version of GitHub Actions Importer.";

    protected override Command GenerateCommand(App app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var command = base.GenerateCommand(app);

        command.AppendPrereleaseOption();

        command.Handler = CommandHandler.Create(app.GetVersionAsync);

        return command;
    }
}
