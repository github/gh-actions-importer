namespace Valet.Interfaces;

public interface IDockerService
{
    Task UpdateImageAsync(string image, string server, string version, string? username, string? password);

    Task ExecuteCommandAsync(string image, params string[] arguments);

    Task VerifyDockerRunningAsync();
}