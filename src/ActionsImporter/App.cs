using ActionsImporter.Interfaces;
using ActionsImporter.Models;

namespace ActionsImporter;

public class App
{
    private const string ActionsImporterImage = "actions-importer/cli";
    private const string ActionsImporterContainerRegistry = "ghcr.io";

    private readonly IDockerService _dockerService;
    private readonly IProcessService _processService;
    private readonly IConfigurationService _configurationService;

    public App(IDockerService dockerService, IProcessService processService, IConfigurationService configurationService)
    {
        _dockerService = dockerService;
        _processService = processService;
        _configurationService = configurationService;
    }

    public async Task<int> UpdateActionsImporterAsync(string? username = null, string? password = null, bool passwordStdin = false)
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
            ActionsImporterImage,
            ActionsImporterContainerRegistry,
            "latest",
            username,
            password,
            passwordStdin
        );

        return 0;
    }

    public async Task<int> ExecuteActionsImporterAsync(string[] args)
    {
        await _dockerService.VerifyDockerRunningAsync().ConfigureAwait(false);
        await _dockerService.VerifyImagePresentAsync(
            ActionsImporterImage,
            ActionsImporterContainerRegistry,
            "latest"
        ).ConfigureAwait(false);

        await _dockerService.ExecuteCommandAsync(
            ActionsImporterImage,
            ActionsImporterContainerRegistry,
            "latest",
            args.Select(x => x.EscapeIfNeeded()).ToArray()
        );
        return 0;
    }

    public async Task<int> GetVersionAsync()
    {
        var ghVersion = await _processService.RunAndCaptureAsync("gh", "version");
        var ghActionsImporterVersion = await _processService.RunAndCaptureAsync("gh", "extension list");
        var actionsImporterVersion = await _processService.RunAndCaptureAsync("docker", $"run --rm {ActionsImporterContainerRegistry}/{ActionsImporterImage}:latest version", throwOnError: false);

        var formattedGhVersion = ghVersion.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).FirstOrDefault();
        var formattedGhActionsImporterVersion = ghActionsImporterVersion.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .FirstOrDefault(x => x.Contains("github/gh-actions-importer", StringComparison.Ordinal));
        var formattedActionsImporterVersion = actionsImporterVersion.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).FirstOrDefault() ?? "unknown";

        Console.WriteLine(formattedGhVersion);
        Console.WriteLine(formattedGhActionsImporterVersion);
        Console.WriteLine($"actions-importer/cli\t{formattedActionsImporterVersion}");

        return 0;
    }

    public async Task CheckForUpdatesAsync()
    {
        try
        {
            var latestImageDigestTask = _dockerService.GetLatestImageDigestAsync(ActionsImporterImage, ActionsImporterContainerRegistry);
            var currentImageDigestTask = _dockerService.GetCurrentImageDigestAsync(ActionsImporterImage, ActionsImporterContainerRegistry);

            await Task.WhenAll(latestImageDigestTask, currentImageDigestTask);

            var latestImageDigest = await latestImageDigestTask;
            var currentImageDigest = await currentImageDigestTask;

            if (latestImageDigest != null && currentImageDigest != null && !latestImageDigest.Equals(currentImageDigest, StringComparison.Ordinal))
            {
                Console.WriteLine("A new version of the GitHub Actions Importer is available. Run 'gh actions-importer update' to update.");
            }
        }
        catch (Exception)
        {
            // Let's catch and ignore any exceptions here. We don't want to kill the importer if we failed to check for updates
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
