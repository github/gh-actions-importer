using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Valet.Commands;

public class Version : BaseCommand
{
    protected override string Name => "version";
    protected override string Description => "Check the version of the Valet docker container.";

    protected override Command GenerateCommand(App app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var command = base.GenerateCommand(app);

        command.Handler = CommandHandler.Create(app.GetVersionAsync);

        return command;
    }
}
