// See https://aka.ms/new-console-template for more information

using CommandLine;
using Valet;
using Valet.Models;
using Valet.Services;

var app = new App(
    new DockerService(),
    new AuthenticationService()
);

// TODO: Utilize help menu from Valet itself
await Parser.Default.ParseArguments<UpdateOptions>(args)
    .MapResult(
        (UpdateOptions opts) => app.UpdateValetAsync(opts.Username, opts.Password),
        _ => app.ExecuteValetAsync(args)
    );