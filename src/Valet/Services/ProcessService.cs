using System.Diagnostics;
using Valet.Interfaces;

namespace Valet.Services;

public class ProcessService : IProcessService
{
    public Task RunAsync(
        string filename,
        string arguments,
        string? cwd = null,
        IEnumerable<(string, string)>? environmentVariables = null,
        bool output = true)
    {
        var tcs = new TaskCompletionSource<bool>();
        var cts = new CancellationTokenSource();

        var startInfo = new ProcessStartInfo
        {
            FileName = filename,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            WorkingDirectory = cwd,
            CreateNoWindow = true
        };

        if (environmentVariables != null)
        {
            foreach (var (key, value) in environmentVariables)
            {
                startInfo.EnvironmentVariables.Add(key, value);
            }
        }

        var process = new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true,
        };

        void OnProcessExited(object? sender, EventArgs args)
        {
            process.Exited -= OnProcessExited;

            cts.Cancel();
            if (process.ExitCode == 0)
            {
                tcs.TrySetResult(true);
            }
            else
            {
                var error = process.StandardError.ReadToEnd();
                tcs.TrySetException(new Exception(error));
            }

            process.Dispose();
        }

        process.Exited += OnProcessExited;
        process.Start();

        ReadStream(process.StandardOutput, output, cts.Token);
        ReadStream(process.StandardError, output, cts.Token);

        return tcs.Task;
    }

    private void ReadStream(StreamReader reader, bool output, CancellationToken ctx)
    {
        Task.Run(() =>
        {
            while (!ctx.IsCancellationRequested)
            {
                int current;
                while ((current = reader.Read()) >= 0)
                {
                    if (output)
                        Console.Write((char)current);
                }
            }
        }, ctx);
    }
}