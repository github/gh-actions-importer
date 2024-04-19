using System.CommandLine;

namespace ActionsImporter.Commands.Bamboo;

public static class Common
{
    public static readonly Option<string> AccessToken = new("--bamboo-access-token")
    {
        Description = "Access token for the Bamboo instance.",
        IsRequired = false,
    };

    public static readonly Option<string> InstanceUrl = new("--bamboo-instance-url")
    {
        Description = "The URL of the Bamboo instance.",
        IsRequired = false,
    };

    public static readonly Option<string> Project = new(new[] { "-p", "--project" })
    {
        Description = "The Bamboo project key.",
        IsRequired = false,
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

    public static readonly Option<string> PlanSlug = new(new[] { "-p", "--plan-slug" })
    {
        Description = "The project and plan key in the format 'ProjectKey-PlanKey'.",
        IsRequired = true,
    };

    public static readonly Option<int> DeploymentProjectId = new("--deployment-project-id")
    {
        Description = "The Bamboo deployment project id.",
        IsRequired = true,
    };
}
