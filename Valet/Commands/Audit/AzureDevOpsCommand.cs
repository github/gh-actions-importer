using System.CommandLine;
using Valet.Commands.Common;
using Valet.Handlers;

namespace Valet.Commands.Audit;

public class AzureDevOpsCommand : ContainerCommand
{
    public AzureDevOpsCommand(string[] args)
        : base(args)
    {
    }
    
    protected override string Name => "azure-devops";
    protected override string Description => "An audit will output a list of data used in an Azure DevOps instance.";

    protected override List<Option> Options => new()
    {
        AzureDevOps.Organization,
        AzureDevOps.Project,
        AzureDevOps.InstanceUrl,
        AzureDevOps.AccessToken
    };
}