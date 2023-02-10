using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Jenkins;

public class Audit : ContainerCommand
{
    public Audit(string[] args) : base(args)
    {
    }

    protected override string Name => "jenkins";
    protected override string Description => "An audit will output a list of data used in a Jenkins instance.";
    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.InstanceUrl,
        Common.Username,
        Common.AccessToken,
        Common.JenkinsfileAccessToken,
        Common.ConfigFilePath
    );
}
