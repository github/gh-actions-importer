// See https://aka.ms/new-console-template for more information

using CommandLine;
using Valet.Models;
using Valet.Services;

static void WithNotParsed(IEnumerable<Error> e, string[] args)
{
    
    Console.WriteLine(string.Join(' ', args));
}

// TODO: Utilize help menu from Valet itself
Parser.Default.ParseArguments<UpdateOptions>(args)
    .WithParsed<UpdateOptions>(async o =>
    {
        var updateService = new UpdateService();
        var result = await updateService.UpdateValetAsync();
    }).WithNotParsed(e => WithNotParsed(e, args));