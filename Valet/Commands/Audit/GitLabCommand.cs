using System.CommandLine;
using Valet.Commands.Common;
using Valet.Handlers;

namespace Valet.Commands.Audit;

public class GitLabCommand : ContainerCommand
{
    public GitLabCommand(string[] args) : base(args)
    {
    }
    
    protected override string Name => "gitlab";
    protected override string Description => "An audit will output a list of data used in a GitLab instance.";
    
    private static readonly Option<FileInfo> ConfigFilePath = new Option<FileInfo>("--config-file-path")
    {
        Description = "The file path corresponding to the GitLab configuration file.",
        IsRequired = false,
    };
    
    protected override List<Option> Options => new()
    {
        GitLab.InstanceUrl,
        GitLab.AccessToken,
        GitLab.Namespace,
        ConfigFilePath
    };
}