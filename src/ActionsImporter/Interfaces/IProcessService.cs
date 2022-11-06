namespace ActionsImporter.Interfaces;

public interface IProcessService
{
    Task RunAsync(
        string filename,
        string arguments,
        string? cwd = null,
        IEnumerable<(string, string)>? environmentVariables = null,
        bool output = true
    );

    Task<(string standardOutput, string standardError, int exitCode)> RunAndCaptureAsync(
        string filename,
        string arguments,
        string? cwd = null,
        IEnumerable<(string, string)>? environmentVariables = null,
        bool throwOnError = true,
        string? inputForStdIn = null
    );
}
