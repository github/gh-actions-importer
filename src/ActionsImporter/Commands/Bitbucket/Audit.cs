using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Bitbucket;

public class Audit : ContainerCommand
{
    public Audit(string[] args) : base(args)
    {
    }

    protected override string Name => "bitbucket";
    protected override string Description => "An audit will output a list of data used in a Bitbucket instance.";
    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.AccessToken,
        Common.Workspace,
        Common.Project,
        Common.ConfigFilePath
    );
}
