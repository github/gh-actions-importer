using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Bamboo.DeploymentPlan;

public class Migrate : ContainerCommand
{
    public Migrate(string[] args) : base(args)
    {
    }

    protected override string Name => "deployment";
    protected override string Description => "Target a deployment plan";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.DeploymentProjectId
    );
}
