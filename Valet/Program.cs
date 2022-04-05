// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using Valet;
using Valet.Commands;
using Valet.Services;

var processService = new ProcessService();

var app = new App(
    new DockerService(processService),
    new AuthenticationService()
);

var command = new RootCommand
{
    new Update().Command(app),
    new Audit(args).Command(app),
    new DryRun(args).Command(app),
    new Migrate(args).Command(app),
    new Forecast(args).Command(app)
};

command.AddGlobalOption(
    new Option<DirectoryInfo>(new[] { "--output-dir", "-o" })
    {
        IsRequired = true,
        Description = "The location for any output files."
    }
);

await command.InvokeAsync(args);