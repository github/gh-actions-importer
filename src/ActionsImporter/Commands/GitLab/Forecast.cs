using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.GitLab;

public class Forecast : ContainerCommand
{
    public Forecast(string[] args) : base(args)
    {
    }

    protected override string Name => "gitlab";
    protected override string Description => "Forecasts GitHub Actions usage from historical GitLab pipeline utilization.";

    private static readonly Option<FileInfo[]> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path(s) to existing jobs data.",
        IsRequired = false,
        AllowMultipleArgumentsPerToken = true,
    };

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.Namespace,
        Common.InstanceUrl,
        Common.AccessToken,
        SourceFilePath
    );
}
