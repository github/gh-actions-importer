using System.CommandLine;
using Valet.Handlers;

namespace Valet.Commands.AzureDevOps;

public class DryRun : BaseCommand
{
    private readonly string[] _args;

    public DryRun(string[] args)
    {
        _args = args;
    }

    protected override string Name => "azure-devops";
    protected override string Description => "Convert an Azure DevOps pipeline to a GitHub Actions workflow and output it's yaml file.";

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);

        command.AddGlobalOption(Common.PipelineId);
        command.AddGlobalOption(Common.InstanceUrl);
        command.AddGlobalOption(Common.Organization);
        command.AddGlobalOption(Common.Project);
        command.AddGlobalOption(Common.AccessToken);

        command.AddCommand(new Pipeline.DryRun(_args).Command(app));
        command.AddCommand(new Release.DryRun(_args).Command(app));

        return command;
    }
}