using System.CommandLine;

namespace Valet.Commands.Travis;

public class Forecast : ContainerCommand
{
    public Forecast(string[] args) : base(args)
    {
    }

    protected override string Name => "travis-ci";
    protected override string Description => "Forecasts GitHub Actions usage from historical Travis CI pipeline utilization.";

    protected override List<Option> Options => new()
    {
        Common.Organization,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.SourceFilePath
    };
}
