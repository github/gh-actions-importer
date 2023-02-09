using System.CommandLine;

namespace ActionsImporter.Commands.Circle;

public static class Common
{
    public static readonly Option<string> InstanceUrl = new(new[] { "-u", "--circle-ci-instance-url" })
    {
        Description = "The URL of the CircleCI instance.",
        IsRequired = false,
    };

    public static readonly Option<string> AccessToken = new(new[] { "-t", "--circle-ci-access-token" })
    {
        Description = "Access token for the CircleCI instance.",
        IsRequired = false,
    };

    public static readonly Option<string> Organization = new(new[] { "-g", "--circle-ci-organization" })
    {
        Description = "The CircleCI organization name.",
        IsRequired = false,
    };

    public static readonly Option<string> Provider = new(new[] { "--circle-ci-provider" })
    {
        Description = "The version control system provider for the pipeline.",
        IsRequired = false,
    };

    public static readonly Option<string> SourceGitHubAccessToken = new(new[] { "-s", "--circle-ci-source-github-access-token" })
    {
        Description = "Access token for the source GitHub instance (if different than the `--github-access-token` value).",
        IsRequired = false,
    };

    public static readonly Option<string> SourceGitHubInstanceUrl = new(new[] { "-i", "--circle-ci-source-github-instance-url" })
    {
        Description = "The URL of the source GitHub instance (if different than the `--github-instance-rul` value).",
        IsRequired = false,
    };

    public static readonly Option<string> Project = new(new[] { "--circle-ci-project", "-p" })
    {
        Description = "The CircleCI project name.",
        IsRequired = false,
    };

    public static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the CircleCI workflow file.",
        IsRequired = false,
    };

    public static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path to the GitHub Actions Importer configuration file.",
        IsRequired = false,
    };

}
