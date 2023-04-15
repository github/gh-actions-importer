using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Travis;

public class Migrate : ContainerCommand
{
    public Migrate(string[] args) : base(args)
    {
    }

    protected override string Name => "travis-ci";
    protected override string Description => "Convert a Travis CI pipeline to a GitHub Actions workflow and and open a pull request with the changes.";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.Repository,
        Common.Organization,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.SourceGitHubInstanceUrl,
        Common.SourceGitHubAccessToken,
        Common.SourceFilePath,
        Common.ConfigFilePath
    );
}
