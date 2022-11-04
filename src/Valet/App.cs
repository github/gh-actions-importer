using Valet.Interfaces;
using Valet.Models;

namespace Valet;

public class App
{
    private const string ValetImage = "valet-customers/valet-cli";
    private const string ValetContainerRegistry = "ghcr.io";

    private readonly IDockerService _dockerService;
    private readonly IProcessService _processService;
    private readonly IConfigurationService _configurationService;

    public App(IDockerService dockerService, IProcessService processService, IConfigurationService configurationService)
    {
        _dockerService = dockerService;
        _processService = processService;
        _configurationService = configurationService;
    }

    public async Task<int> UpdateValetAsync(string? username = null, string? password = null, bool passwordStdin = false)
    {
        await _dockerService.VerifyDockerRunningAsync().ConfigureAwait(false);

        var environmentVariables = await _configurationService.ReadCurrentVariablesAsync().ConfigureAwait(false);

        username ??= Environment.GetEnvironmentVariable("GHCR_USERNAME");
        password ??= Environment.GetEnvironmentVariable("GHCR_PASSWORD");

        if (username == null)
            environmentVariables.TryGetValue("GHCR_USERNAME", out username);

        if (password == null)
            environmentVariables.TryGetValue("GHCR_PASSWORD", out password);

        await _dockerService.UpdateImageAsync(
            ValetImage,
            ValetContainerRegistry,
            "latest",
            username,
            password,
            passwordStdin
        );

        return 0;
    }

    public async Task<int> ExecuteValetAsync(string[] args)
    {
        await _dockerService.VerifyDockerRunningAsync().ConfigureAwait(false);
        await _dockerService.VerifyImagePresentAsync(
            ValetImage,
            ValetContainerRegistry,
            "latest"
        ).ConfigureAwait(false);

        await _dockerService.ExecuteCommandAsync(
            ValetImage,
            ValetContainerRegistry,
            "latest",
            args.Select(x => x.EscapeIfNeeded()).ToArray()
        );
        return 0;
    }

    public async Task<int> GetVersionAsync()
    {
        var (standardOutput, standardError, exitCode) = await _processService.RunAndCaptureAsync("gh", "version");
        var ghValetVersion = await _processService.RunAndCaptureAsync("gh", "extension list");
        var valetVersion = await _processService.RunAndCaptureAsync("docker", $"run --rm {ValetContainerRegistry}/{ValetImage}:latest version", throwOnError: false);

        var formattedGhVersion = standardOutput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).FirstOrDefault();
        var formattedGhValetVersion = ghValetVersion.standardOutput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .FirstOrDefault(x => x.Contains("github/gh-valet", StringComparison.Ordinal));
        var formattedValetVersion = valetVersion.standardOutput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).FirstOrDefault() ?? "unknown";

        Console.WriteLine(formattedGhVersion);
        Console.WriteLine(formattedGhValetVersion);
        Console.WriteLine($"valet-cli\t{formattedValetVersion}");

        return 0;
    }

    public async Task CheckForUpdatesAsync()
    {
        try
        {
            var latestImageDigestTask = _dockerService.GetLatestImageDigestAsync(ValetImage, ValetContainerRegistry);
            var currentImageDigestTask = _dockerService.GetCurrentImageDigestAsync(ValetImage, ValetContainerRegistry);

            await Task.WhenAll(latestImageDigestTask, currentImageDigestTask);

            var latestImageDigest = await latestImageDigestTask;
            var currentImageDigest = await currentImageDigestTask;

            if (latestImageDigest != null && currentImageDigest != null && !latestImageDigest.Equals(currentImageDigest, StringComparison.Ordinal))
            {
                Console.WriteLine("A new version of the Valet CLI is available. Run 'gh valet update' to update.");
            }
        }
        catch (Exception)
        {
            // Let's catch and ignore any exceptions here. We don't want to kill Valet if we failed to check for updates
            // We can add reporting here in the future to alert us of any issues
        }
    }

    public async Task<int> ConfigureAsync()
    {
        var currentVariables = await _configurationService.ReadCurrentVariablesAsync().ConfigureAwait(false);
        var newVariables = _configurationService.GetUserInput();
        var mergedVariables = _configurationService.MergeVariables(currentVariables, newVariables);
        await _configurationService.WriteVariablesAsync(mergedVariables);

        Console.WriteLine("Environment variables successfully updated.");
        return 0;
    }
}
