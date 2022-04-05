using CommandLine;

namespace Valet.Models;

public class ExecuteOptions
{
    [Value(1)]
    public string[]? Arguments { get; set; }
}