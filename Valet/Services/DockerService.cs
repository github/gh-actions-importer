using System.Diagnostics;
using Valet.Interfaces;

namespace Valet.Services;

public class DockerService : IDockerService
{
    private readonly IProcessService _processService;

    private readonly string[] _valetEnvVars =
    {
        "GH_ACCESS_TOKEN", "GH_INSTANCE_URL", "GITHUB_ACCESS_TOKEN", "GITHUB_INSTANCE_URL",
        "JENKINSFILE_ACCESS_TOKEN", "JENKINS_USERNAME", "JENKINS_ACCESS_TOKEN", "JENKINS_INSTANCE_URL",
        "TRAVIS_CI_ACCESS_TOKEN", "TRAVIS_CI_INSTANCE_URL", "TRAVIS_CI_SOURCE_GITHUB_ACCESS_TOKEN", "TRAVIS_CI_SOURCE_GITHUB_INSTANCE_URL", "TRAVIS_CI_ORGANIZATION",
        "CIRCLE_CI_ACCESS_TOKEN", "CIRCLE_CI_INSTANCE_URL", "CIRCLE_CI_ORGANIZATION", "CIRCLE_CI_PROVIDER",
        "GITLAB_INSTANCE_URL", "GITLAB_ACCESS_TOKEN",
        "AZURE_DEVOPS_ACCESS_TOKEN", "AZURE_DEVOPS_PROJECT", "AZURE_DEVOPS_ORGANIZATION", "AZURE_DEVOPS_INSTANCE_URL",
        "YAML_VERBOSITY", "HTTP_PROXY", "HTTPS_PROXY", "NO_PROXY", "OCTOKIT_PROXY", "OCTOKIT_SSL_VERIFY_MODE",
    };

    public DockerService(IProcessService processService)
    {
        _processService = processService;
    }

    public async Task<bool> UpdateImageAsync(string image, string server, string version, string? username, string? password)
    {
        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
        {
            var authenticated = await _processService.RunAsync(
                "docker",
                $"login {server} --password {password} --username {username}"
            ).ConfigureAwait(false);

            if (!authenticated)
            {
                return false;
            }
        }
        else
        {
            Console.WriteLine("No GHCR credentials provided.");
        }

        return await _processService.RunAsync(
            "docker",
            $"pull {server}/{image}:{version}"
        );
    }

    public Task<bool> ExecuteCommandAsync(string image, params string[] arguments)
    {
        var valetArguments = new List<string>();
        valetArguments.Add("run --rm");
        valetArguments.AddRange(GetEnvironmentVariableArguments());
        valetArguments.Add($"-v \"{Directory.GetCurrentDirectory()}\":/data");
        valetArguments.Add(image);
        valetArguments.AddRange(arguments);

        Debug.WriteLine(string.Join(' ', valetArguments));

        return _processService.RunAsync(
            "docker",
            string.Join(' ', valetArguments),
            Directory.GetCurrentDirectory(),
            new[] { ("MSYS_NO_PATHCONV", "1") }
        );
    }

    public async Task VerifyDockerRunningAsync()
    {
        // TODO: Verify this is cross platform
        var result = await _processService.RunAsync(
            "docker",
            "info",
            output: false
        );

        if (!result)
        {
            throw new Exception("Please ensure docker is installed and the docker daemon is running");
        }
    }

    private IEnumerable<string> GetEnvironmentVariableArguments()
    {
        if (File.Exists(".env.local"))
        {
            yield return "--env-file .env.local";
        }

        foreach (var env in _valetEnvVars)
        {
            var value = Environment.GetEnvironmentVariable(env);

            if (string.IsNullOrWhiteSpace(value)) continue;

            var key = env;
            if (key.StartsWith("GH_"))
                key = key.Replace("GH_", "GITHUB_");

            yield return $"--env {key}={value}";
        }
    }
}