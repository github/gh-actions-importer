using System.CommandLine;
using ActionsImporter.Handlers;

namespace ActionsImporter.Commands;

public class Configure : BaseCommand
{
    private readonly string[] _args;
    protected override string Name => "configure";
    protected override string Description => "Start an interactive prompt to configure credentials used to authenticate with your CI server(s).";

    public static readonly Option<bool> OptionalFeaturesOption = new(new[] { "--features" })
    {
        Description = "Configure the feature flags for GitHub Actions Importer."
    };

    public Configure(string[] args)
    {
        _args = args;
    }

    protected override Command GenerateCommand(App app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var command = base.GenerateCommand(app);

        command.AddGlobalOption(OptionalFeaturesOption);
        command.SetHandler(new ConfigureHandler(app).Run(_args));

        return command;
    }
}
