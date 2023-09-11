using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Bitbucket;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "bitbucket";
    protected override string Description => "Convert a Bitbucket pipeline to a GitHub Actions workflow and output the yaml file.";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.Workspace,
        Common.Repository,
        Common.AccessToken,
        Common.SourceFilePath,
        Common.ConfigFilePath
    );
}
