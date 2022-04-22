using System.CommandLine;
namespace Valet.Commands;

public class Version : ContainerCommand
{
    private readonly string[] _args;

    public Version(string[] args)
        : base(args)
    {
        _args = args;
    }

    protected override string Name => "version";
    protected override string Description => "Check the version of the Valet docker container.";

    protected override List<Option> Options { get; } = new();
}