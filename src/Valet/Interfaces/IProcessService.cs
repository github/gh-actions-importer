namespace Valet.Interfaces;

public interface IProcessService
{
    Task<bool> RunAsync(
        string filename,
        string arguments,
        string? cwd = null,
        IEnumerable<(string, string)>? environmentVariables = null,
        bool output = true
    );
}