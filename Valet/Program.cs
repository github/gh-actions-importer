// See https://aka.ms/new-console-template for more information

using CommandLine;
using CommandLine.Text;
using Valet;
using Valet.Models;
using Valet.Services;

var processService = new ProcessService();

var app = new App(
    new DockerService(processService),
    new AuthenticationService()
);


var parser = new Parser(with => with.HelpWriter = null);
var parserResult = parser.ParseArguments<UpdateOptions, ExecuteOptions>(args);

// TODO: Utilize help menu from Valet itself
await parserResult.WithNotParsedAsync(errs =>
    {
        return app.ExecuteValetAsync(args);
    });

await parserResult.WithParsedAsync<UpdateOptions>(options => app.UpdateValetAsync(options.Username, options.Password)); 
    
    
        // (UpdateOptions opts) => app.UpdateValetAsync(opts.Username, opts.Password),
        // (ExecuteOptions opts) => Task.FromResult(1),
        // _ =>
        // {
        //     var helpText = new HelpText
        //     {
        //         Heading = "Tool tool usage",
        //         AdditionalNewLineAfterOption = false,
        //         AddDashesToOption = true
        //     };
        //
        //     Console.Error.WriteLine(helpText);
        //     
        //     return app.ExecuteValetAsync(args);
        // });