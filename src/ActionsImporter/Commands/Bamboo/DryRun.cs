using System.CommandLine;

namespace ActionsImporter.Commands.Bamboo;

public class DryRun : BaseCommand
{
    private readonly string[] _args;

    public DryRun(string[] args)
    {
        _args = args;
    }

    protected override string Name => "bamboo";
    protected override string Description => "Convert a Bamboo pipeline to a GitHub Actions workflow and output its yaml file.";

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);

        command.AddGlobalOption(Common.AccessToken);
        command.AddGlobalOption(Common.InstanceUrl);
        command.AddGlobalOption(Common.Project);

        command.AddCommand(new BuildPlan.DryRun(_args).Command(app));
        command.AddCommand(new DeploymentPlan.DryRun(_args).Command(app));

        return command;
    }
}
