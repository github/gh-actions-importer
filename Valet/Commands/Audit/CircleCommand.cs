using System.CommandLine;
using Valet.Commands.Common;

namespace Valet.Commands.Audit;

public class CircleCommand : ContainerCommand
{
    public CircleCommand(string[] args) : base(args)
    {
    }
    
    protected override string Name => "circle-ci";
    protected override string Description => "An audit will output a list of data used in a Circle CI instance.";
    
    private static readonly Option<FileInfo> ConfigFilePath = new Option<FileInfo>("--config-file-path")
    {
        Description = "The file path corresponding to the Circle CI configuration file.",
        IsRequired = false,
    };
    
    protected override List<Option> Options => new()
    {
        Circle.CircleInstanceUrl,
        Circle.CircleAccessToken,
        Circle.CircleOrganization,
        Circle.CircleSourceGitHubAccessToken,
        Circle.CircleSourceGitHubInstanceUrl,
        ConfigFilePath
    };
}