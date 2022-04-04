using System.CommandLine;

namespace Valet.Commands.Common;

public static class Travis
{
    public static readonly Option<string> TravisInstanceUrl = new Option<string>(new[] {"-u", "--travis-ci-instance-url"})
    {
        Description = "The URL of the Travis CI instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> TravisAccessToken = new Option<string>(new[] {"-t", "--travis-ci-access-token"})
    {
        Description = "Access token for the Travis CI instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> TravisOrganization = new Option<string>(new[] {"-g", "--travis-ci-organization"})
    {
        Description = "The Travis CI organization name.",
        IsRequired = false,
    };
    
    public static readonly Option<string> TravisSourceGitHubAccessToken = new Option<string>(new[] {"-s", "--travis-ci-source-github-access-token"})
    {
        Description = "Access token for the source GitHub instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> TravisSourceGitHubInstanceUrl = new Option<string>(new[] {"-i", "--travis-ci-source-github-instance-url"})
    {
        Description = "The URL of the source GitHub instance.",
        IsRequired = false,
    };
}