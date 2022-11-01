using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Jenkins;

public class Forecast : ContainerCommand
{
    public Forecast(string[] args) : base(args)
    {
    }

    protected override string Name => "jenkins";
    protected override string Description => "Forecasts GitHub Actions usage from historical Jenkins pipeline utilization.";

    private static readonly Option<string[]> FoldersOption = new(new[] { "-f", "--folders" })
    {
        Description = "Folders to forecast in the instance",
        IsRequired = false,
        AllowMultipleArgumentsPerToken = true
    };

    private static readonly Option<FileInfo[]> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path(s) to existing jobs data.",
        IsRequired = false,
        AllowMultipleArgumentsPerToken = true,
    };

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.InstanceUrl,
        Common.Username,
        Common.AccessToken,
        FoldersOption,
        SourceFilePath
    );
}
