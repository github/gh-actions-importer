using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Bamboo.BuildPlan;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "build";
    protected override string Description => "Target a build plan";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.ConfigFilePath,
        Common.SourceFilePath,
        Common.PlanSlug
    );
}
