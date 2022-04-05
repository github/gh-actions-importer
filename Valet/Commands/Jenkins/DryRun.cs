using System.CommandLine;

namespace Valet.Commands.Jenkins;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }
    
    protected override string Name => "jenkins";
    protected override string Description => "Convert a Jenkins job to a GitHub Actions workflow and output it's yaml file.";
    
    private static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the Jenkinsfile.",
        IsRequired = false,
    };
    
    private static readonly Option<string> SourceUrl = new(new[] {"--source-url", "-s"})
    {
        Description = "The URL of the Jenkins job to migrate.",
        IsRequired = true,
    };
    
    protected override List<Option> Options => new()
    {
        SourceUrl,
        Common.InstanceUrl,
        Common.Username,
        Common.AccessToken,
        Common.JenkinsfileAccessToken,
        SourceFilePath
    };
}