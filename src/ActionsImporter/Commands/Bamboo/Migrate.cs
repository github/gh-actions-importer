using System.CommandLine;

namespace ActionsImporter.Commands.Bamboo;

public class Migrate : BaseCommand
{
    private readonly string[] _args;

    public Migrate(string[] args)
    {
        _args = args;
    }

    protected override string Name => "bamboo";
    protected override string Description => "Convert a Bamboo pipeline to a GitHub Actions workflow and open a pull request with the changes.";

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);

        command.AddGlobalOption(Common.AccessToken);
        command.AddGlobalOption(Common.InstanceUrl);
        command.AddGlobalOption(Common.Project);

        command.AddCommand(new BuildPlan.Migrate(_args).Command(app));
        command.AddCommand(new DeploymentPlan.Migrate(_args).Command(app));

        return command;
    }
}
