using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Threading.Tasks;
using ActionsImporter.Interfaces;
using ActionsImporter.Services;
using NUnit.Framework;

namespace ActionsImporter.UnitTests.Services;

[TestFixture]
public class ConfigurationServiceTests
{
#pragma warning disable CS8618
    private IConfigurationService _configurationService;
#pragma warning restore CS8618

    [SetUp]
    public void BeforeEachTest()
    {
        _configurationService = new ConfigurationService();
    }

    [Test]
    public void MergeVariables_OverwritesExisting()
    {
        // Arrange
        var currentVariables = ImmutableDictionary<string, string>.Empty
            .Add("FOO", "current")
            .Add("BAR", "current")
            .Add("BAZ", "current");

        var newVariables = ImmutableDictionary<string, string>.Empty
            .Add("FOO", "new")
            .Add("BAR", "new")
            .Add("BAN", "new");

        var expectedVariables = ImmutableDictionary<string, string>.Empty
            .Add("FOO", "new")
            .Add("BAR", "new")
            .Add("BAZ", "current")
            .Add("BAN", "new");

        // Act
        var result = _configurationService.MergeVariables(
            currentVariables,
            newVariables
            );

        // Assert
        Assert.AreEqual(expectedVariables, result);
    }

    [Test]
    public async Task ReadCurrentVariablesAsync_FileDoesNotExist_ReturnsEmptyVariables()
    {
        // Arrange
        var expectedResult = new Dictionary<string, string>();

        // Act
        var result = await _configurationService.ReadCurrentVariablesAsync("this-does-not-exist");

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public async Task ReadCurrentVariablesAsync_FileExists_ReturnsVariables()
    {
        // Arrange
        var filePath = Path.Combine(Path.GetTempPath(), "gh-actions-importer.tests", ".env.local");
        var directory = Path.GetDirectoryName(filePath)!;

        var contents = @" 
USERNAME=mona
PASSWORD=hunter2 
EMPTY=

MALFORMED=TRUE=
WITH_QUOTES=""value""
  LEADING_SPACE= value
";
        Directory.CreateDirectory(directory);
        await File.WriteAllTextAsync(filePath, contents);

        var expectedResult = new Dictionary<string, string>
        {
            { "USERNAME", "mona" },
            { "PASSWORD", "hunter2" },
            { "WITH_QUOTES", "\"value\"" },
            { "LEADING_SPACE", "value" },
        };

        // Act
        var result = await _configurationService.ReadCurrentVariablesAsync(filePath);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
