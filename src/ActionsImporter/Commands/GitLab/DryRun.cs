using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.GitLab;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "gitlab";
    protected override string Description => "Convert a GitLab pipeline to a GitHub Actions workflow and output the yaml file.";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.Namespace,
        Common.Project,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.SourceFilePath,
        Common.ConfigFilePath
    );
}
