using System.CommandLine;
namespace Valet.Commands;

public class Forecast : BaseCommand
{
    private readonly string[] _args;

    public Forecast(string[] args)
    {
        _args = args;
    }

    protected override string Name => "forecast";
    protected override string Description => "Forecasts GitHub actions usage from historical pipeline utilization.";

    private static readonly Option<DateTime> StartDate = new("--start-date", getDefaultValue: () => DateTime.Now.AddDays(-7))
    {
        Description = "The start date of the forecast analysis in YYYY-MM-DD format.",
        IsRequired = false,
    };

    private static readonly Option<int> TimeSlice = new("--time-slice", getDefaultValue: () => 60)
    {
        Description = "The time slice in seconds to use for computing concurrency metrics.",
        IsRequired = false,
    };

    private static readonly Option<FileInfo[]> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path(s) to existing jobs data.",
        IsRequired = false,
        AllowMultipleArgumentsPerToken = true
    };

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);
        command.AppendGeneralRequiredOptions();

        command.AddGlobalOption(StartDate);
        command.AddGlobalOption(TimeSlice);
        command.AddGlobalOption(SourceFilePath);

        command.AppendGeneralOptions();

        command.AddCommand(new AzureDevOps.Forecast(_args).Command(app));
        command.AddCommand(new Jenkins.Forecast(_args).Command(app));
        command.AddCommand(new GitLab.Forecast(_args).Command(app));
        command.AddCommand(new Circle.Forecast(_args).Command(app));
        command.AddCommand(new Travis.Forecast(_args).Command(app));

        return command;
    }
}