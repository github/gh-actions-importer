using Valet.Interfaces;

namespace Valet;

public class App
{
    const string ValetImage = "valet-customers/valet-cli";
    const string ValetContainerRegistry = "ghcr.io";

    private readonly IDockerService _dockerService;
    private readonly IConfigurationService _configurationService;

    public App(IDockerService dockerService, IConfigurationService configurationService)
    {
        _dockerService = dockerService;
        _configurationService = configurationService;
    }

    public async Task<int> UpdateValetAsync(string? username = null, string? password = null, bool passwordStdin = false)
    {
        await _dockerService.VerifyDockerRunningAsync().ConfigureAwait(false);

        username ??= Environment.GetEnvironmentVariable("GHCR_USERNAME");
        password ??= Environment.GetEnvironmentVariable("GHCR_PASSWORD");

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
            args
        );
        return 0;
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