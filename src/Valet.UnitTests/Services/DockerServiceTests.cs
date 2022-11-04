﻿using System;
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

    [TearDown]
    public void AfterEachTest()
    {
        Environment.SetEnvironmentVariable("DOCKER_ARGS", null);
        Environment.SetEnvironmentVariable("GH_ACCESS_TOKEN", null);
        Environment.SetEnvironmentVariable("GH_INSTANCE_URL", null);
        Environment.SetEnvironmentVariable("JENKINS_ACCESS_TOKEN", null);
    }

    [Test]
    public async Task UpdateImageAsync_NoCredentialsProvided_PullsLatest_ReturnsTrue()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"pull {server}/{image}:{version} --quiet",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>(),
                null
            )
        ).ReturnsAsync(("", "", 0));

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
    public void UpdateImageAsync_InvalidScopes_ThrowsUnauthorized()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";
        var username = "dwight";
        var password = "assistant_regional_manager";

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"login {server} --username {username} --password-stdin",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                false,
                password
            )
        ).ReturnsAsync(("Login Successful", "", 0));

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"pull {server}/{image}:{version} --quiet",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>(),
                null
            )
        ).ReturnsAsync(("", "Error response from daemon: denied", 1));

        // Act/Assert
        Assert.ThrowsAsync<Exception>(() => _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            username,
            password
        ),
@"You are not authorized to access Valet yet. Please ensure you've completed the following:
- Requested access to Valet and received onboarding instructions via email.
- Accepted all of the repository invites sent after being onboarded.
- The GitHub personal access token used above contains the 'read:packages' scope.");
        _processService.VerifyAll();
    }

    [Test]
    public void UpdateImageAsync_InvalidCredentialsProvided_ThrowsUnauthorized()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";
        var username = "dwight";
        var password = "assistant_to_the_regional_manager";

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"login {server} --username {username} --password-stdin",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                false,
                password
            )
        ).ReturnsAsync(("", $"Error response from daemon: Get \"https://{server}/v2/\": denied: denied", 1));

        // Act/Assert
        Assert.ThrowsAsync<Exception>(() => _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            username,
            password
        ),
@"You are not authorized to access Valet yet. Please ensure you've completed the following:
- Requested access to Valet and received onboarding instructions via email.
- Accepted all of the repository invites sent after being onboarded.");
        _processService.VerifyAll();
    }

    [Test]
    public void UpdateImageAsync_Unauthenticated_ThrowsUnauthorized()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"pull {server}/{image}:{version} --quiet",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>(),
                null
            )
        ).ReturnsAsync(("", $"Error response from daemon: Head \"https://{server}/v2/valet-customers/valet-cli/manifests/latest\": unauthorized", 1));

        // Act/Assert
        Assert.ThrowsAsync<Exception>(() => _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            null,
            null
        ),
            @"You are not authorized to access Valet yet. Please ensure you've completed the following:
- Requested access to Valet and received onboarding instructions via email.
- Accepted all of the repository invites sent after being onboarded.
- The GitHub personal access token used above contains the 'read:packages' scope.");
        _processService.VerifyAll();
    }

    [Test]
    public void UpdateImageAsync_InvalidCredentialsProvided_ThrowsUnknownError()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";
        var username = "dwight";
        var password = "assistant_to_the_regional_manager";

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"login {server} --username {username} --password-stdin",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                false,
                password
            )
        ).ReturnsAsync(("", "Unknown error", 1));

        // Act/Assert
        Assert.ThrowsAsync<Exception>(() => _dockerService.UpdateImageAsync(
            image,
            server,
            version,
            username,
            password
        ), $"There was an error authenticating with the {server} docker repository.\nError: Unknown error");
        _processService.VerifyAll();
    }

    [Test]
    public async Task UpdateImageAsync_ValidCredentialsProvided_ReturnsSuccessful()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";
        var username = "dwight";
        var password = "assistant_to_the_regional_manager";

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"login {server} --username {username} --password-stdin",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>(),
                password
            )
        ).ReturnsAsync(("Login Succeeded", "", 0));

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"pull {server}/{image}:{version} --quiet",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>(),
                null
            )
        ).ReturnsAsync(("", "", 0));

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
                new[] { new ValueTuple<string, string>("MSYS_NO_PATHCONV", "1") },
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
                new[] { new ValueTuple<string, string>("MSYS_NO_PATHCONV", "1") },
                true
            )
        ).Returns(Task.CompletedTask);

        // Act
        await _dockerService.ExecuteCommandAsync(image, server, version, arguments);

        // Assert
        _processService.VerifyAll();
    }

    [Test]
    public async Task ExecuteCommandAsync_InvokesDocker_WithAdditionalDockerArguments_ReturnsTrue()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var version = "latest";
        var arguments = new[] { "run", "this", "command" };

        Environment.SetEnvironmentVariable("DOCKER_ARGS", "--network=host");

        _processService.Setup(handler =>
            handler.RunAsync(
                "docker",
                $"run --rm -t --network=host -v \"{Directory.GetCurrentDirectory()}\":/data {server}/{image}:{version} {string.Join(' ', arguments)}",
                Directory.GetCurrentDirectory(),
                new[] { new ValueTuple<string, string>("MSYS_NO_PATHCONV", "1") },
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

    [Test]
    public async Task GetCurrentImageDigest_ParsesDigestCorrectly()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"image inspect --format={{{{.Id}}}} {server}/{image}:latest",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>(),
                null
            )
        ).ReturnsAsync(("sha256:67eed1493c461efd993be9777598a456562f4e0c6b0bddcb19d819220a06dd4b", "", 0));

        // Act
        var result = await _dockerService.GetCurrentImageDigestAsync(image, server);

        // Assert
        Assert.AreEqual("67eed1493c461efd993be9777598a456562f4e0c6b0bddcb19d819220a06dd4b", result);
        _processService.VerifyAll();
    }

    [Test]
    public async Task GetLatestImageDigest_ParsesDigestCorrectly()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";
        var manifestResult = @"
{
        ""schemaVersion"": 2,
        ""mediaType"": ""application/vnd.docker.distribution.manifest.v2+json"",
        ""config"": {
                ""mediaType"": ""application/vnd.docker.container.image.v1+json"",
                ""size"": 8933,
                ""digest"": ""sha256:4256ea72fd01deac3e967f6b19f907587dcd6f0a976301f1aecc73dc6f146a4a""
        },
        ""layers"": [
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 2811969,
                        ""digest"": ""sha256:540db60ca9383eac9e418f78490994d0af424aab7bf6d0e47ac8ed4e2e9bcbba""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 1218679,
                        ""digest"": ""sha256:98a867505730167ce0636f0811cb765ebbde11bf979c1a9839f6915f2fc3b85b""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 222,
                        ""digest"": ""sha256:69c77620f6108dc0610cba72f77d68c271fb1b14cb0800a7a8b6aaeb8950fec9""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 23207774,
                        ""digest"": ""sha256:9b370d66bb9903810edd365042a2f34b7a59947c942741ab947cec1b00dc738d""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 171,
                        ""digest"": ""sha256:d9f4ad4e4f54edfb60dcbf3da39a0d79bb818be470a76b7aa5d940214fd6817b""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 26977223,
                        ""digest"": ""sha256:d75779add5fb23230f53fc7b04edd78f0ac04328d54ae8b10ffc24ae8ce53fae""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 442547,
                        ""digest"": ""sha256:8e1571077aa30d750261fb58d29aca90235515a7ee2b35160bc907bfed0a0353""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 4346366,
                        ""digest"": ""sha256:c7ca3c3f89d58546450869925f28bcb901a395d5b2f7fbf013bba9d8bca353af""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 32,
                        ""digest"": ""sha256:4f4fb700ef54461cfa02571ae0db9a0dc1e0cdb5577484a6d75e68dc38e8acc1""
                }
        ]
}";

        _processService.Setup(handler =>
            handler.RunAndCaptureAsync(
                "docker",
                $"manifest inspect {server}/{image}:latest",
                It.IsAny<string?>(),
                It.IsAny<IEnumerable<(string, string)>?>(),
                It.IsAny<bool>(),
                null
            )
        ).ReturnsAsync((manifestResult, "", 0));

        // Act
        var result = await _dockerService.GetLatestImageDigestAsync(image, server);

        // Assert
        Assert.AreEqual("4256ea72fd01deac3e967f6b19f907587dcd6f0a976301f1aecc73dc6f146a4a", result);
        _processService.VerifyAll();
    }
}
