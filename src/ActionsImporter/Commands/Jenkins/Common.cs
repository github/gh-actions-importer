using System.CommandLine;

namespace ActionsImporter.Commands.Jenkins;

public static class Common
{
    public static readonly Option<string> InstanceUrl = new(new[] { "-u", "--jenkins-instance-url" })
    {
        Description = "The URL of the Jenkins CI instance.",
        IsRequired = false,
    };

    public static readonly Option<string> AccessToken = new(new[] { "-t", "--jenkins-access-token" })
    {
        Description = "Access token for the Jenkins instance.",
        IsRequired = false,
    };

    public static readonly Option<string> Username = new(new[] { "-n", "--jenkins-username" })
    {
        Description = "Username for the Jenkins instance.",
        IsRequired = false,
    };

    public static readonly Option<string> JenkinsfileAccessToken = new("--jenkinsfile-access-token")
    {
        Description = "Access token for the GitHub repo containing the job's Jenkinsfile for a pipeline (if different than the `--github-access-token` value).",
        IsRequired = false,
    };

    public static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the Jenkinsfile.",
        IsRequired = false,
    };

    public static readonly Option<string> SourceUrl = new(new[] { "--source-url", "-s" })
    {
        Description = "The URL of the Jenkins job to migrate.",
        IsRequired = true,
    };

    public static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path to the GitHub Actions Importer configuration file.",
        IsRequired = false,
    };

}
