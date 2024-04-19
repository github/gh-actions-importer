using System.CommandLine;

namespace ActionsImporter.Commands;

public class DryRun : BaseCommand
{
    private readonly string[] _args;

    public DryRun(string[] args)
    {
        _args = args;
    }

    protected override string Name => "dry-run";
    protected override string Description => "Convert a pipeline to a GitHub Actions workflow and output its yaml file.";

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);
        command.AppendCommonOptions();

        command.AddCommand(new AzureDevOps.DryRun(_args).Command(app));
        command.AddCommand(new Bamboo.DryRun(_args).Command(app));
        command.AddCommand(new Bitbucket.DryRun(_args).Command(app));
        command.AddCommand(new Circle.DryRun(_args).Command(app));
        command.AddCommand(new GitLab.DryRun(_args).Command(app));
        command.AddCommand(new Jenkins.DryRun(_args).Command(app));
        command.AddCommand(new Travis.DryRun(_args).Command(app));

        return command;
    }
}
