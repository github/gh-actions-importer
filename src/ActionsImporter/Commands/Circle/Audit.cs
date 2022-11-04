using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Circle;

public class Audit : ContainerCommand
{
    public Audit(string[] args) : base(args)
    {
    }

    protected override string Name => "circle-ci";
    protected override string Description => "An audit will output a list of data used in a CircleCI instance.";

    private static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path corresponding to the CircleCI configuration file.",
        IsRequired = false,
    };

    private static readonly Option<FileInfo> IncludeFrom = new("--include-from")
    {
        Description = "The file path containing a list of line-delimited repositories to include in the audit.",
        IsRequired = false,
    };

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.InstanceUrl,
        Common.AccessToken,
        Common.Organization,
        Common.SourceGitHubAccessToken,
        Common.SourceGitHubInstanceUrl,
        ConfigFilePath,
        IncludeFrom
    );
}
