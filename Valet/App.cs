using Valet.Interfaces;

namespace Valet;

public class App
{
    const string VALET_IMAGE_LOCATION = "ghcr.io/valet-customers/valet-cli:latest";
    private const string VALET_CONTAINER_REGISTRY = "ghcr.io";

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
        username ??= Environment.GetEnvironmentVariable("GHCR_USERNAME");
        password ??= Environment.GetEnvironmentVariable("GHCR_PASSWORD");

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            (username, password) = await _authenticationService.GetAccessTokenAsync();
        }
        
        var result = await _dockerService.UpdateImageAsync(
            VALET_IMAGE_LOCATION,
            VALET_CONTAINER_REGISTRY,
            username,
            password
        );

        return result ? 0 : 1;
    }

    public Task<int> ExecuteValetAsync(string[] args)
    {
        Console.WriteLine(string.Join(' ', args));
        return Task.FromResult(1);
    }
}