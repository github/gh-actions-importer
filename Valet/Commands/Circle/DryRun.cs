using System.CommandLine;

namespace Valet.Commands.Circle;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }
    
    protected override string Name => "circle-ci";
    protected override string Description => "Convert a CircleCI pipeline to GitHub Actions workflows and output the yaml file(s).";
    
    private static readonly Option<string> Project = new(new[] {"--circle-ci-project", "-p"})
    {
        Description = "The Circle CI project name.",
        IsRequired = false,
    };
    
    private static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the Circle CI workflow file.",
        IsRequired = false,
    };
    
    protected override List<Option> Options => new()
    {
        Common.Organization,
        Project,
        Common.Provider,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.SourceGitHubAccessToken,
        Common.SourceGitHubInstanceUrl,
        SourceFilePath,
    };
}