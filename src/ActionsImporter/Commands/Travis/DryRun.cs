using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Travis;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "travis-ci";
    protected override string Description => "Convert a Travis CI pipeline to a GitHub Actions workflow and output its yaml file.";

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
