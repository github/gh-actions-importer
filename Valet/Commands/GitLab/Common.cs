using System.CommandLine;

namespace Valet.Commands.GitLab;

public static class Common
{
    public static readonly Option<string> InstanceUrl = new("--gitlab-instance-url")
    {
        Description = "The URL of the GitLab instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> Namespace = new("--namespace")
    {
        Description = "The GitLab namespace(s).",
        IsRequired = false,
    };
    
    public static readonly Option<string> AccessToken = new("--gitlab-access-token")
    {
        Description = "Access token for the GitLab instance.",
        IsRequired = false,
    };
}