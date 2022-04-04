using System.CommandLine;
using Valet.Commands.Common;

namespace Valet.Commands.Audit;

public class JenkinsCommand : ContainerCommand
{
    public JenkinsCommand(string[] args) : base(args)
    {
    }
    
    protected override string Name => "jenkins";
    protected override string Description => "An audit will output a list of data used in a Jenkins instance.";
    
    private static readonly Option<FileInfo> ConfigFilePath = new Option<FileInfo>("--config-file-path")
    {
        Description = "The file path corresponding to the Jenkins configuration file.",
        IsRequired = false,
    };
    
    protected override List<Option> Options => new()
    {
        Jenkins.InstanceUrl,
        Jenkins.Username,
        Jenkins.AccessToken,
        Jenkins.JenkinsfileAccessToken,
        ConfigFilePath
    };
}