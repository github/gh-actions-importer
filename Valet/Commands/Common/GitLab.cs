using System.CommandLine;

namespace Valet.Commands.Common;

public static class GitLab
{
    public static readonly Option<string> GitLabInstanceUrl = new Option<string>("--gitlab-instance-url")
    {
        Description = "The URL of the GitLab instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> Namespace = new Option<string>("--namespace")
    {
        Description = "The GitLab namespace(s).",
        IsRequired = false,
    };
    
    public static readonly Option<string> GitLabAccessToken = new Option<string>("--gitlab-access-token")
    {
        Description = "Access token for the GitLab instance.",
        IsRequired = false,
    };
}