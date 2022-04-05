using System.CommandLine;

namespace Valet.Commands.Travis;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }
    
    protected override string Name => "travis-ci";
    protected override string Description => "Convert a Travis CI pipeline to a GitHub Actions workflow and output it's yaml file.";
    
    private static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the Travis CI pipeline file.",
        IsRequired = false,
    };

    private static readonly Option<string> Repository = new(new[] { "--travis-ci-repository", "-r" })
    {
        Description = "The Travis CI repository name.",
        IsRequired = true,
    };
    
    protected override List<Option> Options => new()
    {
        Repository,
        Common.Organization,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.SourceGitHubInstanceUrl,
        Common.SourceGitHubAccessToken,
        SourceFilePath
    };
}