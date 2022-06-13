using System.CommandLine;

namespace Valet.Commands.Jenkins;

public class Forecast : ContainerCommand
{
    public Forecast(string[] args) : base(args)
    {
    }

    protected override string Name => "jenkins";
    protected override string Description => "Forecasts GitHub Actions usage from historical Jenkins pipeline utilization.";

    private static readonly Option<string[]> FoldersOption = new(new[] { "-f", "--folders" })
    {
        Description = "Folders to forecast in the instance",
        IsRequired = false,
        AllowMultipleArgumentsPerToken = true
    };

    protected override List<Option> Options => new()
    {
        Common.InstanceUrl,
        Common.Username,
        Common.AccessToken,
        FoldersOption
    };
}