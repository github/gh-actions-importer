using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Parsing;
using ActionsImporter;
using ActionsImporter.Commands;
using ActionsImporter.Models;
using ActionsImporter.Services;
using Version = ActionsImporter.Commands.Version;

var processService = new ProcessService();
var configurationService = new ConfigurationService();

var app = new App(
    new DockerService(processService, new RuntimeService()),
    processService,
    new ConfigurationService(),
    await configurationService.ReadCurrentVariablesAsync()
);

string welcomeMessage = @"GitHub Actions Importer helps you plan, test, and automate your migration to GitHub Actions.

Please share your feedback @ https://gh.io/ghaimporterfeedback";

var command = new RootCommand(welcomeMessage)
{
    new Update().Command(app),
    new Version().Command(app),
    new Configure(args).Command(app),
    new Audit(args).Command(app),
    new Forecast(args).Command(app),
    new DryRun(args).Command(app),
    new Migrate(args).Command(app),
    new ListFeatures(args).Command(app)
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

var parsedArguments = parser.Parse(args);
app.IsPrerelease = parsedArguments.HasOption(Common.Prerelease);
app.NoHostNetwork = parsedArguments.HasOption(Common.NoHostNetwork);

var testCommandOnly = Environment.GetEnvironmentVariable("TEST_COMMAND_ONLY");
if (testCommandOnly != null && testCommandOnly.ToUpperInvariant() == "TRUE")
{
    if (parsedArguments.Errors.Count > 0)
    {
        foreach (var error in parsedArguments.Errors)
        {
            Console.WriteLine(error.Message);
        }
        return 1;
    }

    Console.WriteLine("Valid command!");
    return 0;
}

try
{
    if (!Array.Exists(args, x => x == "update"))
    {
        await app.CheckForUpdatesAsync();
    }

    await parser.InvokeAsync(app, args, parsedArguments.HasOption(Common.Experimental));
    return 0;
}
catch (Exception e)
{
    await Console.Error.WriteLineAsync(e.Message);
    return 1;
}
