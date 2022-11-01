using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace ActionsImporter.Commands;

public class Configure : BaseCommand
{
    protected override string Name => "configure";
    protected override string Description => "Start an interactive prompt to configure credentials used to authenticate with your CI server(s).";

    protected override Command GenerateCommand(App app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var command = base.GenerateCommand(app);

        command.Handler = CommandHandler.Create(app.ConfigureAsync);

        return command;
    }
}
