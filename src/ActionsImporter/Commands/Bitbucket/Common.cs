using System.CommandLine;

namespace ActionsImporter.Commands.Bitbucket;

public static class Common
{


    public static readonly Option<string> AccessToken = new("--bitbucket-access-token")
    {
        Description = "Access token for the Bitbucket instance.",
        IsRequired = false,
    };

    public static readonly Option<string> Workspace = new(new[] { "--workspace", "-w" })
    {
        Description = "The Bitbucket workspace name.",
        IsRequired = true,
    };

    public static readonly Option<string> Project = new(new[] { "--project-key", "-p" })
    {
        Description = "The Bitbucket project key.",
        IsRequired = false,
    };

    public static readonly Option<string> Repository = new(new[] { "--repository", "-r" })
    {
        Description = "The Bitbucket repository name.",
        IsRequired = true,
    };

    public static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path to the GitHub Actions Importer configuration file.",
        IsRequired = false,
    };

    public static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the Bamboo pipeline file.",
        IsRequired = false,
    };
}
