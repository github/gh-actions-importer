using System.CommandLine;

namespace Valet.Commands.Common;

public static class Jenkins
{
    public static readonly Option<string> JenkinsInstanceUrl = new Option<string>(new[] {"-u", "--jenkins-instance-url"})
    {
        Description = "The URL of the Jenkins CI instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> JenkinsAccessToken = new Option<string>(new[] {"-t", "--jenkins-access-token"})
    {
        Description = "Access token for the Jenkins instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> JenkinsUsername = new Option<string>(new[] {"-n", "--jenkins-username"})
    {
        Description = "Username for the Jenkins instance.",
        IsRequired = false,
    };
    
    public static readonly Option<string> JenkinsfileAccessToken = new Option<string>("--jenkinsfile-access-token")
    {
        Description = "Access token for the GitHub repo containing the job's Jenkinsfile for a pipeline.",
        IsRequired = false,
    };
}