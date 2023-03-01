namespace ActionsImporter.Interfaces;

public interface IDockerService
{
    Task UpdateImageAsync(string image, string server, string version);

    Task ExecuteCommandAsync(string image, string server, string version, params string[] arguments);

    Task VerifyDockerRunningAsync();

    Task VerifyImagePresentAsync(string image, string server, string version, bool isPrerelease);

    Task<string?> GetLatestImageDigestAsync(string image, string server);

    Task<string?> GetCurrentImageDigestAsync(string image, string server);
}
