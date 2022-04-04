using System.CommandLine;

namespace Valet.Commands.Travis;

public class Audit : ContainerCommand
{
    public Audit(string[] args) : base(args)
    {
    }
    
    protected override string Name => "travis-ci";
    protected override string Description => "An audit will output a list of data used in a Travis CI instance.";
    
    private static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path corresponding to the Travis CI configuration file.",
        IsRequired = false,
    };
    private static readonly Option<bool> AllowInactiveRepositories = new("--allow-inactive-repositories")
    {
        Description = "Include inactive travis repositories.",
        IsRequired = false
    };
    
    protected override List<Option> Options => new()
    {
        Travis.Common.InstanceUrl,
        Travis.Common.AccessToken,
        Travis.Common.Organization,
        Travis.Common.SourceGitHubInstanceUrl,
        Travis.Common.SourceGitHubAccessToken,
        ConfigFilePath,
        AllowInactiveRepositories
    };
}