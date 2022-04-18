using Valet.Interfaces;

namespace Valet;

public class App
{
    const string ValetImage = "valet-customers/valet-cli";
    const string ValetContainerRegistry = "ghcr.io";

    private readonly IDockerService _dockerService;

    public App(IDockerService dockerService)
    {
        _dockerService = dockerService;
    }

    public async Task<int> UpdateValetAsync(string? username = null, string? password = null)
    {
        await _dockerService.VerifyDockerRunningAsync().ConfigureAwait(false);

        username ??= Environment.GetEnvironmentVariable("GHCR_USERNAME");
        password ??= Environment.GetEnvironmentVariable("GHCR_PASSWORD");

        var result = await _dockerService.UpdateImageAsync(
            ValetImage,
            ValetContainerRegistry,
            "latest",
            username,
            password
        );

        return result ? 0 : 1;
    }

    public async Task<int> ExecuteValetAsync(string[] args)
    {
        await _dockerService.VerifyDockerRunningAsync().ConfigureAwait(false);
        var result = await _dockerService.ExecuteCommandAsync($"{ValetContainerRegistry}/{ValetImage}:latest", args);
        return result ? 0 : 1;
    }
}