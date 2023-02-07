using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Jenkins;

public class Migrate : ContainerCommand
{
    public Migrate(string[] args) : base(args)
    {
    }

    protected override string Name => "jenkins";
    protected override string Description => "Convert a Jenkins job to a GitHub Actions workflow and open a pull request with the changes.";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.SourceUrl,
        Common.InstanceUrl,
        Common.Username,
        Common.AccessToken,
        Common.JenkinsfileAccessToken,
        Common.SourceFilePath,
        Common.ConfigFilePath
    );
}
