using System.CommandLine;

namespace Valet.Commands.Common;

public static class Circle
{
    public static readonly Option<string> InstanceUrl = new(new[] {"-u", "--circle-ci-instance-url"})
    {
        Description = "The URL of the Circle CI instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> AccessToken = new(new[] {"-t", "--circle-ci-access-token"})
    {
        Description = "Access token for the Circle CI instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> Organization = new(new[] {"-g", "--circle-ci-organization"})
    {
        Description = "The Circle CI organization name.",
        IsRequired = false,
    };
    
    public static readonly Option<string> SourceGitHubAccessToken = new(new[] {"-s", "--circle-ci--srouce-github-access-token"})
    {
        Description = "Access token for the source GitHub instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> SourceGitHubInstanceUrl = new(new[] {"-i", "--circle-ci-source-github-instance-url"})
    {
        Description = "The URL of the source GitHub instance.",
        IsRequired = false,
    };
}