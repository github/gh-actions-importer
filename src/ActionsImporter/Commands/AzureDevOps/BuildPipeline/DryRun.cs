using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.AzureDevOps.BuildPipeline;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "pipeline";
    protected override string Description => "Target a designer or YAML pipeline";

    private static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path corresponding to the Azure DevOps configuration file.",
        IsRequired = false,
    };

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        ConfigFilePath
    );
}
