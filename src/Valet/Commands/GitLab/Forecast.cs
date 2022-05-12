using System.CommandLine;

namespace Valet.Commands.GitLab;

public class Forecast : ContainerCommand
{
    public Forecast(string[] args) : base(args)
    {
    }

    protected override string Name => "gitlab";
    protected override string Description => "Forecasts GitHub Actions usage from historical GitLab pipeline utilization.";

    protected override List<Option> Options => new()
    {
        Common.Namespace,
        Common.InstanceUrl,
        Common.AccessToken
    };
}