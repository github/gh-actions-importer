using Docker.DotNet;
using Docker.DotNet.Models;
using Valet.Interfaces;
using Valet.Models;

namespace Valet.Services;

public class DockerService : IDockerService
{
    private readonly DockerClient _client;

    private readonly string[] _valetEnvVars = {
        "GH_ACCESS_TOKEN", "GH_INSTANCE_URL", "GITHUB_ACCESS_TOKEN", "GITHUB_INSTANCE_URL",
        "JENKINSFILE_ACCESS_TOKEN", "JENKINS_USERNAME", "JENKINS_ACCESS_TOKEN", "JENKINS_INSTANCE_URL",
        "TRAVIS_CI_ACCESS_TOKEN", "TRAVIS_CI_INSTANCE_URL", "TRAVIS_CI_SOURCE_GITHUB_ACCESS_TOKEN", "TRAVIS_CI_SOURCE_GITHUB_INSTANCE_URL", "TRAVIS_CI_ORGANIZATION",
        "CIRCLE_CI_ACCESS_TOKEN", "CIRCLE_CI_INSTANCE_URL", "CIRCLE_CI_ORGANIZATION", "CIRCLE_CI_PROVIDER",
        "GITLAB_INSTANCE_URL", "GITLAB_ACCESS_TOKEN",
        "AZURE_DEVOPS_ACCESS_TOKEN", "AZURE_DEVOPS_PROJECT", "AZURE_DEVOPS_ORGANIZATION", "AZURE_DEVOPS_INSTANCE_URL",
        "YAML_VERBOSITY", "HTTP_PROXY", "HTTPS_PROXY", "NO_PROXY", "OCTOKIT_PROXY", "OCTOKIT_SSL_VERIFY_MODE",
    };
    
    public DockerService()
    {
        // TODO: Raise error if docker daemon not started
        _client = new DockerClientConfiguration()
            .CreateClient();
    }

    public async Task<bool> UpdateImageAsync(string image, string server, string version, string username, string password)
    {
        await _client.Images.CreateImageAsync(
            new ImagesCreateParameters
            {
                FromImage = $"{server}/{image}:{version}"
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

    public async Task<bool> ExecuteCommandAsync(string image, params string[] arguments)
    {
        // MSYS_NO_PATHCONV=1 docker run --rm $dockerArgs --env INSTALLATION_ID="$INSTALLATION_ID" $DOCKER_OPTIONS -v "$VALET_LOCAL_FOLDER"":/data" "$VALET_IMAGE":"$VALET_IMAGE_VERSION" "$@"
        var container = await _client.Containers.CreateContainerAsync(
            new CreateContainerParameters
            {
                Image = image,
                HostConfig = new HostConfig
                {
                    Binds = new[] { $"{Directory.GetCurrentDirectory()}:/data" },
                    AutoRemove = true,
                },
                Env = GetEnvironmentVariables().ToArray()
            }
        ).ConfigureAwait(false);

        await _client.Containers.StartContainerAsync(
            container.ID,
            new ContainerStartParameters()
        );
        
        
        return true;
    }

    private IEnumerable<string> GetEnvironmentVariables()
    {
        if (File.Exists(".env.local"))
        {
            foreach (var line in File.ReadAllLines(".env.local"))
            {
                var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                yield return $"{parts[0]}={parts[1]}";
            }
        }

        foreach (var env in _valetEnvVars)
        {
            var value = Environment.GetEnvironmentVariable(env);

            if (!string.IsNullOrWhiteSpace(value))
            {
                var key = env;
                // TODO: This can probably be cleaner
                if (key.StartsWith("GH_"))
                    key = key.Replace("GH_", "GITHUB_");
                    
                yield return $"{key}={value}";
            }
        }

        var installationId = Environment.GetEnvironmentVariable("INSTALLATION_ID");
        if (installationId == null)
        {
            installationId = "get_from_client";
        }

        yield return $"INSTALLATION_ID={installationId}";
    }
}