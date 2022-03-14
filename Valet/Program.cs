// See https://aka.ms/new-console-template for more information

using CommandLine;
using Valet.Models;

static void WithNotParsed(IEnumerable<Error> e, string[] args)
{
    Console.WriteLine(string.Join(' ', args));
}

static void Update(UpdateOptions o)
{
    Console.WriteLine("Updating Valet");
}

// TODO: Utilize help menu from Valet itself
Parser.Default.ParseArguments<UpdateOptions>(args)
    .WithParsed<UpdateOptions>(Update)
    .WithNotParsed(e => WithNotParsed(e, args));