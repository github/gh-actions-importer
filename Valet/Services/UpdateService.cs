using Docker.DotNet;
using Docker.DotNet.Models;

namespace Valet.Services;

public class Progress : IProgress<JSONMessage>
{
    public void Report(JSONMessage value)
    {
        Console.WriteLine(string.IsNullOrEmpty(value.ErrorMessage)
            ? $"{value.ID} {value.Status} {value.ProgressMessage}"
            : value.ErrorMessage
        );
    }
}

public class UpdateService
{
    private DockerClient _client;

    public UpdateService()
    {
        // TODO: Raise error if docker daemon not started
        _client = new DockerClientConfiguration()
            .CreateClient();
    }

    public async Task<bool> UpdateValetAsync()
    {
        var task = _client.Images.CreateImageAsync(
            new ImagesCreateParameters
            {
                FromImage = "ghcr.io/valet-customers/valet-cli:latest",
            },
            new AuthConfig
            {
                Username = Environment.GetEnvironmentVariable("GHCR_USERNAME"),
                Password = Environment.GetEnvironmentVariable("GHCR_PASSWORD"),
                ServerAddress = "ghcr.io"
            },
            progress: new Progress());

        task.Wait();

        return true;
    }
}