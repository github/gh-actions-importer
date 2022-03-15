using Docker.DotNet;
using Docker.DotNet.Models;
using Valet.Interfaces;
using Valet.Models;

namespace Valet.Services;

public class DockerService : IDockerService
{
    private readonly DockerClient _client;

    public DockerService()
    {
        // TODO: Raise error if docker daemon not started
        _client = new DockerClientConfiguration()
            .CreateClient();
    }

    public async Task<bool> UpdateImageAsync(string image, string server, string username, string password)
    {
        await _client.Images.CreateImageAsync(
            new ImagesCreateParameters
            {
                FromImage = image,
            },
            new AuthConfig
            {
                Username = username,
                Password = password,
                ServerAddress = server
            },
            progress: new Progress()
        ).ConfigureAwait(false);

        return true;
    }
}