using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Valet.Interfaces;
using Valet.Services;

namespace Valet.UnitTests.Services;

[TestFixture]
public class DockerServiceTests
{
#pragma warning disable CS8618
    private DockerService _dockerService;
    private Mock<IProcessService> _processService;
#pragma warning restore CS8618

    [SetUp]
    public void BeforeEachTest()
    {
        _processService = new Mock<IProcessService>();
        _dockerService = new DockerService(_processService.Object);
    }

    [Test]
    public async Task UpdateImageAsync_NoCredentialsProvided_PullsLatest_ReturnsTrue()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";

        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"pull {server}/{image}:{version}",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>()
            )
        ).Returns(Task.CompletedTask);

        // Act
        await _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            null,
            null
        );

        // Assert
        _processService.VerifyAll();
    }

    [Test]
    public async Task UpdateImageAsync_InvalidCredentialsProvided_ReturnsFalse()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";
        var username = "dwight";
        var password = "assistant_regional_manager";

        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"login {server} --password {password} --username {username}",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>()
            )
        ).Returns(Task.CompletedTask);

        // Act
        await _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            username,
            password
        );

        // Assert
        _processService.VerifyAll();
    }

    [Test]
    public async Task UpdateImageAsync_ValidCredentialsProvided_ReturnsTrue()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";
        var username = "dwight";
        var password = "assistant_to_the_regional_manager";

        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"login {server} --password {password} --username {username}",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>()
            )
        ).Returns(Task.CompletedTask);

        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"pull {server}/{image}:{version}",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>()
            )
        ).Returns(Task.CompletedTask);

        // Act
        await _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            username,
            password
        );

        // Assert
        _processService.VerifyAll();
    }

    [Test]
    public async Task ExecuteCommandAsync_InvokesDocker_ReturnsTrue()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";
        var arguments = new[] { "run", "this", "command" };
        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"run --rm -t -v \"{Directory.GetCurrentDirectory()}\":/data {server}/{image}:{version} {string.Join(' ', arguments)}",
                Directory.GetCurrentDirectory(),
                new[] { new System.ValueTuple<string, string>("MSYS_NO_PATHCONV", "1") },
                true
            )
        ).Returns(Task.CompletedTask);

        // Act
        await _dockerService.ExecuteCommandAsync(image, server, version, arguments);

        // Assert
        _processService.VerifyAll();
    }

    [Test]
    public async Task ExecuteCommandAsync_InvokesDocker_WithEnvironmentVariables_ReturnsTrue()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";
        var arguments = new[] { "run", "this", "command" };

        Environment.SetEnvironmentVariable("GH_ACCESS_TOKEN", "foo");
        Environment.SetEnvironmentVariable("GH_INSTANCE_URL", "https://github.fabrikam.com");
        Environment.SetEnvironmentVariable("JENKINS_ACCESS_TOKEN", "bar");

        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"run --rm -t --env GITHUB_ACCESS_TOKEN=foo --env GITHUB_INSTANCE_URL=https://github.fabrikam.com --env JENKINS_ACCESS_TOKEN=bar -v \"{Directory.GetCurrentDirectory()}\":/data {server}/{image}:{version} {string.Join(' ', arguments)}",
                Directory.GetCurrentDirectory(),
                new[] { new System.ValueTuple<string, string>("MSYS_NO_PATHCONV", "1") },
                true
            )
        ).Returns(Task.CompletedTask);

        // Act
        await _dockerService.ExecuteCommandAsync(image, server, version, arguments);

        // Assert
        _processService.VerifyAll();
    }

    [Test]
    public void VerifyDockerRunningAsync_IsRunning_NoException()
    {
        // Arrange
        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                "info",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>()
            )
        ).Returns(Task.CompletedTask);

        // Act, Assert
        Assert.DoesNotThrowAsync(() => _dockerService.VerifyDockerRunningAsync());
    }

    [Test]
    public void VerifyDockerRunningAsync_NotRunning_ThrowsException()
    {
        // Arrange
        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                "info",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>()
            )
        ).ThrowsAsync(new Exception());

        // Act, Assert
        Assert.ThrowsAsync<Exception>(() => _dockerService.VerifyDockerRunningAsync());
    }

    [Test]
    public void VerifyImagePresentAsync_IsPresent_NoException()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";

        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"image inspect {server}/{image}:{version}",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>()
            )
        ).Returns(Task.CompletedTask);

        // Act, Assert
        Assert.DoesNotThrowAsync(() => _dockerService.VerifyImagePresentAsync(image, server, version));
        _processService.VerifyAll();
    }

    [Test]
    public void VerifyImagePresentAsync_NotPresent_ThrowsException()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";

        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"image inspect {server}/{image}:{version}",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>()
            )
        ).ThrowsAsync(new Exception());

        // Act, Assert
        Assert.ThrowsAsync<Exception>(() => _dockerService.VerifyImagePresentAsync(image, server, version));
        _processService.VerifyAll();
    }
}