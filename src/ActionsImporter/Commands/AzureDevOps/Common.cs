using System.CommandLine;

namespace ActionsImporter.Commands.AzureDevOps;

public static class Common
{
    public static readonly Option<string> InstanceUrl = new(new[] { "-u", "--azure-devops-instance-url" })
    {
        Description = "The URL of the Azure DevOps instance.",
        IsRequired = false,
    };

    public static readonly Option<string> Organization = new(new[] { "-g", "--azure-devops-organization" })
    {
        Description = "The Azure DevOps organization name.",
        IsRequired = false,
    };

    public static readonly Option<string> Project = new(new[] { "-p", "--azure-devops-project" })
    {
        Description = "The Azure DevOps project name.",
        IsRequired = false,
    };

    public static readonly Option<string> AccessToken = new(new[] { "-t", "--azure-devops-access-token" })
    {
        Description = "Access token for the Azure DevOps instance.",
        IsRequired = false,
    };

    public static readonly Option<int> PipelineIdRequired = new(new[] { "--pipeline-id", "-i" })
    {
        Description = "The Azure DevOps pipeline ID.",
        IsRequired = true,
    };

    public static readonly Option<int> PipelineIdNotRequired = new(new[] { "--pipeline-id", "-i" })
    {
        Description = "The Azure DevOps pipeline ID.",
        IsRequired = false,
    };

    public static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the Azure DevOps pipeline file.",
        IsRequired = false,
    };
    public static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path to the GitHub Actions Importer configuration file.",
        IsRequired = false,
    };

}
