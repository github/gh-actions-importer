namespace ActionsImporter.Interfaces;

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

    Task<string> RunAndCaptureAsync(
        string filename,
        string arguments,
        string? cwd = null,
        IEnumerable<(string, string)>? environmentVariables = null,
        bool throwOnError = true
    );
}
