using System.CommandLine;

namespace ActionsImporter.Commands.AzureDevOps;

public class Migrate : BaseCommand
{
    private readonly string[] _args;

    public Migrate(string[] args)
    {
        _args = args;
    }

    protected override string Name => "azure-devops";
    protected override string Description => "Convert an Azure DevOps pipeline to a GitHub Actions workflow and open a pull request with the changes.";

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);

        command.AddGlobalOption(Common.InstanceUrl);
        command.AddGlobalOption(Common.Organization);
        command.AddGlobalOption(Common.Project);
        command.AddGlobalOption(Common.AccessToken);

        command.AddCommand(new BuildPipeline.Migrate(_args).Command(app));
        command.AddCommand(new ReleasePipeline.Migrate(_args).Command(app));

        return command;
    }
}
