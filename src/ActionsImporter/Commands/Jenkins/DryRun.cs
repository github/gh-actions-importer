using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Jenkins;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "jenkins";
    protected override string Description => "Convert a Jenkins job to a GitHub Actions workflow and output its yaml file.";

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
