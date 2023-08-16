using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Bitbucket;

public class Bitbucket : ContainerCommand
{
    public Bitbucket(string[] args) : base(args)
    {
    }

    protected override string Name => "bitbucket";
    protected override string Description => "Forecasts GitHub Actions usage from historical Bitbucket pipeline utilization.";

    private static readonly Option<FileInfo[]> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path(s) to existing jobs data.",
        IsRequired = false,
        AllowMultipleArgumentsPerToken = true,
    };

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.Workspace,
        Common.InstanceUrl,
        Common.AccessToken,
        SourceFilePath
    );
}
