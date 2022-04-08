namespace Valet.Interfaces;

public interface IDockerService
{
    Task<bool> UpdateImageAsync(string image, string server, string version, string? username, string? password);

    Task<bool> ExecuteCommandAsync(string image, params string[] arguments);

    Task VerifyDockerRunningAsync();
}