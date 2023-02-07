using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Travis;

public class Audit : ContainerCommand
{
    public Audit(string[] args) : base(args)
    {
    }

    protected override string Name => "travis-ci";
    protected override string Description => "An audit will output a list of data used in a Travis CI instance.";
    private static readonly Option<bool> AllowInactiveRepositories = new("--allow-inactive-repositories")
    {
        Description = "Include inactive travis repositories.",
        IsRequired = false
    };

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.Organization,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.SourceGitHubInstanceUrl,
        Common.SourceGitHubAccessToken,
        Common.ConfigFilePath,
        AllowInactiveRepositories
    );
}
