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
    new DockerService(processService, new RuntimeService()),
    processService,
    new ConfigurationService()
);

string welcomeMessage = @"GitHub Actions Importer helps you plan, test, and automate your migration to GitHub Actions.

Please share your feedback @ https://gh.io/ghaimporterfeedback";

var command = new RootCommand(welcomeMessage)
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
              .Where((_, i) => i != 1) // Remove the usage section (second item in the help layout)
            );
    })
    .UseEnvironmentVariableDirective()
    .RegisterWithDotnetSuggest()
    .UseSuggestDirective()
    .UseTypoCorrections()
    .UseParseErrorReporting()
    .CancelOnProcessTermination()
    .Build();


app.IsPrerelease = parser.Parse(args).HasOption(Common.Prerelease);

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
    await Console.Error.WriteLineAsync(e.Message);
    return 1;
}
