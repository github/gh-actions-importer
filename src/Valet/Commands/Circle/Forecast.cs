using System.CommandLine;

namespace Valet.Commands.Circle;

public class Forecast : ContainerCommand
{
    public Forecast(string[] args) : base(args)
    {
    }

    protected override string Name => "circle-ci";
    protected override string Description => "Forecasts GitHub Actions usage from historical CircleCI pipeline utilization.";

    protected override List<Option> Options => new()
    {
        Common.Organization,
        Common.Project,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.SourceGitHubAccessToken,
        Common.SourceGitHubInstanceUrl,
        Common.SourceFilePath
    };
}
