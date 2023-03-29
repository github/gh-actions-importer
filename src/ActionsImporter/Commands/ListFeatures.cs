using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands;

public class ListFeatures : ContainerCommand
{
    public ListFeatures(string[] args) : base(args)
    {
    }

    protected override string Name => "list-features";
    protected override string Description => "List the available feature flags for GitHub Actions Importer.";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>();
}
