using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Parsing;
using ActionsImporter;
using ActionsImporter.Commands;
using ActionsImporter.Services;
using Version = ActionsImporter.Commands.Version;

var processService = new ProcessService();

var app = new App(
    new DockerService(processService),
    processService,
    new ConfigurationService()
);

var command = new RootCommand("GitHub Actions Importer is a tool to help plan and automate your migration to Actions.")
{
    new Update().Command(app),
    new Version().Command(app),
    new Configure().Command(app),
    new Audit(args).Command(app),
    new Forecast(args).Command(app),
    new DryRun(args).Command(app),
    new Migrate(args).Command(app)
};

var parser = new CommandLineBuilder(command)
    .UseHelp(ctx =>
    {
        ctx.HelpBuilder.CustomizeLayout(_ =>
            HelpBuilder.Default
                .GetLayout()
                .Skip(2)
            );
    })
    .UseEnvironmentVariableDirective()
    .RegisterWithDotnetSuggest()
    .UseSuggestDirective()
    .UseTypoCorrections()
    .UseParseErrorReporting()
    .CancelOnProcessTermination()
    .Build();

try
{
    if (!Array.Exists(args, x => x == "update"))
    {
        await app.CheckForUpdatesAsync();
    }
    await parser.InvokeAsync(args);
    return 0;
}
catch (Exception e)
{
    await Console.Error.WriteAsync(e.Message);
    return 1;
}
