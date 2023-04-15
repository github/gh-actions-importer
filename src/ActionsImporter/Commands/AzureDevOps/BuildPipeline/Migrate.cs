using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.AzureDevOps.BuildPipeline;

public class Migrate : ContainerCommand
{
    public Migrate(string[] args) : base(args)
    {
    }

    protected override string Name => "pipeline";
    protected override string Description => "Target a designer or YAML pipeline";
    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.SourceFilePath,
        Common.ConfigFilePath,
        Common.PipelineIdNotRequired
    );
}
