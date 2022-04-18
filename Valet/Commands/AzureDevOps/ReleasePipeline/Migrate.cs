using System.CommandLine;

namespace Valet.Commands.AzureDevOps.ReleasePipeline;

public class Migrate : ContainerCommand
{
    public Migrate(string[] args) : base(args)
    {
    }

    protected override string Name => "release";
    protected override string Description => "Target a release pipeline";
    protected override List<Option> Options => new();
}