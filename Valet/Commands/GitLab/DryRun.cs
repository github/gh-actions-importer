using System.CommandLine;

namespace Valet.Commands.GitLab;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }
    
    protected override string Name => "gitlab";
    protected override string Description => "Convert a GitLab pipeline to a GitHub Actions workflow and output the yaml file.";
    
    private static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the GitLab CI workflow file.",
        IsRequired = false,
    };
    
    private static readonly Option<string> Project = new("--project")
    {
        Description = "The GitLab project name.",
        IsRequired = true,
    };
    
    protected override List<Option> Options => new()
    {
        Common.Namespace,
        Project,
        Common.InstanceUrl,
        Common.AccessToken,
        SourceFilePath
    };
}