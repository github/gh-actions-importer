using System.Collections.Specialized;
using Valet.Interfaces;

namespace Valet.Services;

using System;
using System.Diagnostics;
using System.Threading.Tasks;

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
        var startInfo = new ProcessStartInfo
        {
            FileName = filename,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            WorkingDirectory = cwd
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
            process.OutputDataReceived -= OnOutputDataReceived;

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

        void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (output) Console.WriteLine(e.Data);
        }

        process.OutputDataReceived += OnOutputDataReceived;
        process.Exited += OnProcessExited;

        process.Start();
        process.BeginOutputReadLine();

        return tcs.Task;
    }
}