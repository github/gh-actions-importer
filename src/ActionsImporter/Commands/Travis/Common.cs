using System.CommandLine;

namespace ActionsImporter.Commands.Travis;

public static class Common
{
    public static readonly Option<string> InstanceUrl = new(new[] { "-u", "--travis-ci-instance-url" })
    {
        Description = "The URL of the Travis CI instance.",
        IsRequired = false,
    };

    public static readonly Option<string> AccessToken = new(new[] { "-t", "--travis-ci-access-token" })
    {
        Description = "Access token for the Travis CI instance.",
        IsRequired = false,
    };

    public static readonly Option<string> Organization = new(new[] { "-g", "--travis-ci-organization" })
    {
        Description = "The Travis CI organization name.",
        IsRequired = false,
    };

    public static readonly Option<string> SourceGitHubAccessToken = new(new[] { "-s", "--travis-ci-source-github-access-token" })
    {
        Description = "Access token for the source GitHub instance (if different than the `--github-access-token` value).",
        IsRequired = false,
    };

    public static readonly Option<string> SourceGitHubInstanceUrl = new(new[] { "-i", "--travis-ci-source-github-instance-url" })
    {
        Description = "The URL of the source GitHub instance (if different than the `--github-instance-url` value).",
        IsRequired = false,
    };

    public static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the Travis CI pipeline file.",
        IsRequired = false,
    };

    public static readonly Option<string> Repository = new(new[] { "--travis-ci-repository", "-r" })
    {
        Description = "The Travis CI repository name.",
        IsRequired = true,
    };
    public static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path to the GitHub Actions Importer configuration file.",
        IsRequired = false,
    };
}
