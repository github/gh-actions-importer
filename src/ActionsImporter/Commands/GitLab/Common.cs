using System.CommandLine;

namespace ActionsImporter.Commands.GitLab;

public static class Common
{
    public static readonly Option<string> InstanceUrl = new("--gitlab-instance-url")
    {
        Description = "The URL of the GitLab instance.",
        IsRequired = false,
    };

    public static readonly Option<string[]> Namespace = new(new[] { "--namespace", "-n" })
    {
        Description = "The GitLab namespace(s).",
        IsRequired = false,
        AllowMultipleArgumentsPerToken = true
    };

    public static readonly Option<string> AccessToken = new("--gitlab-access-token")
    {
        Description = "Access token for the GitLab instance.",
        IsRequired = false,
    };

    public static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the GitLab CI workflow file.",
        IsRequired = false,
    };

    public static readonly Option<string> Project = new("--project")
    {
        Description = "The GitLab project name.",
        IsRequired = true,
    };

    public static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path to the GitHub Actions Importer configuration file.",
        IsRequired = false,
    };
}
