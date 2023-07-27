using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.GitHub;

public class Forecast : ContainerCommand
{
    public Forecast(string[] args) : base(args)
    {
    }

    protected override string Name => "github";
    protected override string Description => "Forecasts GitHub Actions usage from historical GitHub pipeline utilization.";

    public static readonly Option<string> InstanceUrl = new(new[] { "--github-instance-url", "-u" })
    {
        Description = "The URL of the GitHub instance.",
        IsRequired = false,
    };

    public static readonly Option<string> AccessToken = new(new[] { "--github-access-token", "-t" })
    {
        Description = "Access token for the GitHub instance.",
        IsRequired = false,
    };

    public static readonly Option<string> Organization = new(new[] { "--organization", "-g" })
    {
        Description = "The GitHub organization name.",
        IsRequired = true,
    };

    public static readonly Option<string> Repository = new(new[] { "--repository", "-r" })
    {
        Description = "The GitHub repository name.",
        IsRequired = false,
    };

    private static readonly Option<FileInfo> IncludeFrom = new("--include-from")
    {
        Description = "The file path containing a list of line-delimited repository names to include in the forecast.",
        IsRequired = false,
    };

    private static readonly Option<FileInfo[]> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path(s) to existing jobs data.",
        IsRequired = false,
        AllowMultipleArgumentsPerToken = true,
    };

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        InstanceUrl,
        AccessToken,
        Organization,
        Repository,
        IncludeFrom,
        SourceFilePath
    );
}
