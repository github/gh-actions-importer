using System.CommandLine;

namespace Valet.Commands.Common;

public static class AzureDevOps
{
    public static readonly Option<string> AzureDevOpsInstanceUrl = new Option<string>(new[] { "-u", "--azure-devops-instance-url" })
    {
        Description = "The URL of the Azure DevOps instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> AzureDevOpsOrganization = new Option<string>(new[] { "-g", "--azure-devops-organization" })
    {
        Description = "The Azure DevOps organization name.",
        IsRequired = false,
    };
    
    public static readonly Option<string> AzureDevOpsProject = new Option<string>(new[] { "-p", "--azure-devops-project" })
    {
        Description = "The Azure DevOps project name.",
        IsRequired = false,
    };
    
    public static readonly Option<string> AzureDevOpsAccessToken = new Option<string>(new[] { "-t", "--azure-devops-access-token" })
    {
        Description = "Access token for the Azure DevOps instance.",
        IsRequired = false,
    };
}