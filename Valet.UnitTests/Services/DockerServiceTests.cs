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
    private DockerService _dockerService;
    private Mock<IProcessService> _processService;

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
        ).ReturnsAsync(true);

        // Act
        var result = await _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            null,
            null
        );

        // Assert
        Assert.IsTrue(result);
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
        ).ReturnsAsync(false);

        // Act
        var result = await _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            username,
            password
        );

        // Assert
        Assert.IsFalse(result);
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
        ).ReturnsAsync(true);

        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"pull {server}/{image}:{version}",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>()
            )
        ).ReturnsAsync(true);

        // Act
        var result = await _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            username,
            password
        );

        // Assert
        Assert.IsTrue(result);
        _processService.VerifyAll();
    }

    [Test]
    public async Task ExecuteCommandAsync_InvokesDocker_ReturnsTrue()
    {
        // Arrange
        var image = "ghcr.io/valet-customers/valet-cli:latest";
        var arguments = new[] { "run", "this", "command" };
        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"run --rm -v \"{Directory.GetCurrentDirectory()}\":/data {image} {string.Join(' ', arguments)}",
                Directory.GetCurrentDirectory(),
                new[] { new System.ValueTuple<string, string>("MSYS_NO_PATHCONV", "1") },
                true
            )
        ).ReturnsAsync(true);

        // Act
        var result = await _dockerService.ExecuteCommandAsync(image, arguments);

        // Assert
        Assert.IsTrue(result);
        _processService.VerifyAll();
    }
    
    [Test]
    public async Task ExecuteCommandAsync_InvokesDocker_WithEnvironmentVariables_ReturnsTrue()
    {
        // Arrange
        var image = "ghcr.io/valet-customers/valet-cli:latest";
        var arguments = new[] { "run", "this", "command" };
        
        Environment.SetEnvironmentVariable("GH_ACCESS_TOKEN", "foo");
        Environment.SetEnvironmentVariable("GH_INSTANCE_URL", "https://github.fabrikam.com");
        Environment.SetEnvironmentVariable("JENKINS_ACCESS_TOKEN", "bar");
        
        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"run --rm --env GITHUB_ACCESS_TOKEN=foo --env GITHUB_INSTANCE_URL=https://github.fabrikam.com --env JENKINS_ACCESS_TOKEN=bar -v \"{Directory.GetCurrentDirectory()}\":/data {image} {string.Join(' ', arguments)}",
                Directory.GetCurrentDirectory(),
                new[] { new System.ValueTuple<string, string>("MSYS_NO_PATHCONV", "1") },
                true
            )
        ).ReturnsAsync(true);

        // Act
        var result = await _dockerService.ExecuteCommandAsync(image, arguments);

        // Assert
        Assert.IsTrue(result);
        _processService.VerifyAll();
    }

    [Test]
    public async Task VerifyDockerRunningAsync_IsRunning_NoException()
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
        ).ReturnsAsync(true);

        // Act, Assert
        Assert.DoesNotThrowAsync(() => _dockerService.VerifyDockerRunningAsync());
    }

    [Test]
    public async Task VerifyDockerRunningAsync_NotRunning_ThrowsException()
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
        ).ReturnsAsync(false);

        // Act, Assert
        Assert.ThrowsAsync<Exception>(() => _dockerService.VerifyDockerRunningAsync());
    }
}