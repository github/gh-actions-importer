namespace Valet.Interfaces;

public interface IProcessService
{
    Task RunAsync(
        string filename,
        string arguments,
        string? cwd = null,
        IEnumerable<(string, string)>? environmentVariables = null,
        bool output = true,
        string? inputForStdIn = null
    );
}