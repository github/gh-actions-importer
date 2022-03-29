using System.CommandLine;
using Valet.Handlers;

namespace Valet.Commands.Audit;

public class AzureDevOpsCommand : BaseCommand
{
    private readonly string[] _args;
    
    public AzureDevOpsCommand(string[] args)
    {
        _args = args;
    }
    
    protected override string Name => "azure-devops";
    protected override string Description => "An audit will output a list of data used in an Azure DevOps instance.";

    protected override Command GenerateCommand(App app)
    {
        // TODO: Add service container?
        var command = base.GenerateCommand(app);

        command.AddOption(Common.AzureDevOps.AzureDevOpsOrganization);
        command.AddOption(Common.AzureDevOps.AzureDevOpsProject);
        command.AddOption(Common.AzureDevOps.AzureDevOpsInstanceUrl);
        command.AddOption(Common.AzureDevOps.AzureDevOpsAccessToken);

        command.SetHandler(new ContainerHandler(app).Run(_args));
        
        return command;
    }
}