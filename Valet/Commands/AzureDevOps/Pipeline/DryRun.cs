using System.CommandLine;

namespace Valet.Commands.AzureDevOps.Pipeline;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "pipeline";
    protected override string Description => "Target a designer or YAML pipeline";
    protected override List<Option> Options => new();
}