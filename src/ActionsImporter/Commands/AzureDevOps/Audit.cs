using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.AzureDevOps;

public class Audit : ContainerCommand
{
    public Audit(string[] args)
        : base(args)
    {
    }

    protected override string Name => "azure-devops";
    protected override string Description => "An audit will output a list of data used in an Azure DevOps instance.";
    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.ConfigFilePath,
        Common.Organization,
        Common.Project,
        Common.InstanceUrl,
        Common.AccessToken
    );
}
