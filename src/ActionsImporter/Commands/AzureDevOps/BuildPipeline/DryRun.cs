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

    public static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the Azure DevOps pipeline file.",
        IsRequired = false,
    };

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        SourceFilePath
    );
}
