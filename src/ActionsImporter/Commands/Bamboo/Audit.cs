using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Bamboo;

public class Audit : ContainerCommand
{
    public Audit(string[] args)
        : base(args)
    {
    }

    protected override string Name => "bamboo";
    protected override string Description => "An audit will output a list of data used in a Bamboo instance.";
    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.AccessToken,
        Common.InstanceUrl,
        Common.Project,
        Common.ConfigFilePath
    );
}
