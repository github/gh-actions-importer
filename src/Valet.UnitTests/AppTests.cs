using System;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Valet.Interfaces;
using Valet.Services;

namespace Valet.UnitTests;

[TestFixture]
public class AppTests
{
#pragma warning disable CS8618
    private Mock<IProcessService> _processService;
    private Mock<IDockerService> _dockerService;
    private Mock<IConfigurationService> _configurationService;
    private App _app;
#pragma warning restore CS8618

    [SetUp]
    public void BeforeEachTest()
    {
        _dockerService = new Mock<IDockerService>();
        _processService = new Mock<IProcessService>();
        _configurationService = new Mock<IConfigurationService>();
        _app = new App(_dockerService.Object, _processService.Object, _configurationService.Object);
    }

    [TestCase("4256ea72fd01deac3e967f6b19f907587dcd6f0a976301f1aecc73dc6f146a4a", "4256ea72fd01deac3e967f6b19f907587dcd6f0a976301f1aecc73dc6f146a4a", "")]
    [TestCase("4256ea72fd01deac3e967f6b19f907587dcd6f0a976301f1aecc73dc6f146a4a", "67eed1493c461efd993be9777598a456562f4e0c6b0bddcb19d819220a06dd4b", "A new version of the Valet CLI is available. Run 'gh valet update' to update.\n")]
    public async Task CheckForUpdates_NoUpdatesNeeded(string? latestImage, string? currentImage, string result)
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        _dockerService.Setup(handler =>
            handler.GetLatestImageDigestAsync(image, server)
        ).ReturnsAsync(latestImage);

        _dockerService.Setup(handler =>
            handler.GetCurrentImageDigestAsync(image, server)
        ).ReturnsAsync(currentImage);

        // Act
        await _app.CheckForUpdatesAsync();

        // Assert
        Assert.AreEqual(result, stringWriter.ToString());
        _processService.VerifyAll();
    }

    [Test]
    public async Task CheckForUpdates_RaisesCaughtException()
    {
        // Arrange
        var image = "valet-customers/valet-cli";
        var server = "ghcr.io";

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        _dockerService.Setup(handler =>
            handler.GetLatestImageDigestAsync(image, server)
        ).ThrowsAsync(new Exception());

        // Act
        await _app.CheckForUpdatesAsync();

        // Assert
        Assert.AreEqual("", stringWriter.ToString());
        _processService.VerifyAll();
    }
}