namespace Valet.Interfaces;

public interface IDockerService
{
    Task UpdateImageAsync(string image, string server, string version, string? username, string? password);

    Task ExecuteCommandAsync(string image, string server, string version, params string[] arguments);

    Task VerifyDockerRunningAsync();

    Task VerifyImagePresentAsync(string image, string server, string version);
}