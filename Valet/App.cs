using Valet.Interfaces;

namespace Valet;

public class App
{
    const string ValetImage = "valet-customers/valet-cli";
    const string ValetContainerRegistry = "ghcr.io";

    private readonly IDockerService _dockerService;
    private readonly IAuthenticationService _authenticationService;

    public App(
        IDockerService dockerService,
        IAuthenticationService authenticationService)
    {
        _dockerService = dockerService;
        _authenticationService = authenticationService;
    }

    public async Task<int> UpdateValetAsync(string? username = null, string? password = null)
    {
        var dockerIsRunning = await _dockerService.IsDockerRunningAsync().ConfigureAwait(false);
        if (!dockerIsRunning)
        {
            throw new Exception("Please ensure docker is installed and the docker daemon is running");
        }

        username ??= Environment.GetEnvironmentVariable("GHCR_USERNAME");
        password ??= Environment.GetEnvironmentVariable("GHCR_PASSWORD");

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            (username, password) = await _authenticationService.GetAccessTokenAsync();
        }

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
        var dockerIsRunning = await _dockerService.IsDockerRunningAsync().ConfigureAwait(false);
        if (!dockerIsRunning)
        {
            throw new Exception("Please ensure docker is installed and the docker daemon is running");
        }

        var result = await _dockerService.ExecuteCommandAsync($"{ValetContainerRegistry}/{ValetImage}:latest", args);
        return result ? 0 : 1;
    }
}