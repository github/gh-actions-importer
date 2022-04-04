using System.CommandLine;

namespace Valet.Commands.Common;

public static class Circle
{
    public static readonly Option<string> CircleInstanceUrl = new Option<string>(new[] {"-u", "--circle-ci-instance-url"})
    {
        Description = "The URL of the Circle CI instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> CircleAccessToken = new Option<string>(new[] {"-t", "--circle-ci-access-token"})
    {
        Description = "Access token for the Circle CI instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> CircleOrganization = new Option<string>(new[] {"-g", "--circle-ci-organization"})
    {
        Description = "The Circle CI organization name.",
        IsRequired = false,
    };
    
    public static readonly Option<string> CircleSourceGitHubAccessToken = new Option<string>(new[] {"-s", "--circle-ci--srouce-github-access-token"})
    {
        Description = "Access token for the source GitHub instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> CircleSourceGitHubInstanceUrl = new Option<string>(new[] {"-i", "--circle-ci-source-github-instance-url"})
    {
        Description = "The URL of the source GitHub instance.",
        IsRequired = false,
    };
}