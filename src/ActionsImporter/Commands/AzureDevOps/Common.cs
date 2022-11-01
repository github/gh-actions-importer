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

    public static readonly Option<int> PipelineId = new(new[] { "--pipeline-id", "-i" })
    {
        Description = "The Azure DevOps pipeline id.",
        IsRequired = true,
    };
}
