using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.AzureDevOps.ReleasePipeline;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "release";
    protected override string Description => "Target a release pipeline";
    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.PipelineIdRequired
    );
}
