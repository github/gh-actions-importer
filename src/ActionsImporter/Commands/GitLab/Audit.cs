using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.GitLab;

public class Audit : ContainerCommand
{
    public Audit(string[] args) : base(args)
    {
    }

    protected override string Name => "gitlab";
    protected override string Description => "An audit will output a list of data used in a GitLab instance.";
    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.InstanceUrl,
        Common.AccessToken,
        Common.Namespace,
        Common.ConfigFilePath
    );
}
