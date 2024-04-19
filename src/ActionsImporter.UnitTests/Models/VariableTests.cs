using ActionsImporter.Models;
using NUnit.Framework;

namespace ActionsImporter.UnitTests.Models;

[TestFixture]
public class VariableTests
{
    [TestCase(Provider.GitHub, "GitHub")]
    [TestCase(Provider.AzureDevOps, "Azure DevOps")]
    [TestCase(Provider.CircleCI, "CircleCI")]
    [TestCase(Provider.GitLabCI, "GitLab CI")]
    [TestCase(Provider.Jenkins, "Jenkins")]
    [TestCase(Provider.TravisCI, "Travis CI")]
    [TestCase(Provider.Bamboo, "Bamboo")]
    [TestCase(Provider.Bitbucket, "Bitbucket")]
    public void ProviderName_ValidName_ReturnsExpected(Provider provider, string providerName)
    {
        // Arrange
        var variable = new Variable("FOO", provider, "");

        // Act
        Assert.AreEqual(providerName, variable.ProviderName);
    }

    [TestCase("USERNAME", false)]
    [TestCase("PERSONAL_ACCESS_TOKEN", true)]
    [TestCase("SOME_PASSWORD", true)]
    public void IsPassword_ReturnsExpected(string key, bool isPassword)
    {
        // Arrange
        var variable = new Variable(key, Provider.GitHub, "");

        // Act
        Assert.AreEqual(isPassword, variable.IsPassword);
    }

    [TestCase(false, null, null)]
    [TestCase(false, "default value", null)]
    [TestCase(true, null, null)]
    [TestCase(true, "default value", "(default value)")]
    public void Placeholder_ReturnsExpected(bool isPassword, string? defaultValue, string expectedPlaceholder)
    {
        // Arrange
        var key = isPassword ? "PERSONAL_ACCESS_TOKEN" : "USERNAME";
        var variable = new Variable(key, Provider.GitHub, "message", defaultValue);

        // Act
        Assert.AreEqual(expectedPlaceholder, variable.Placeholder);
    }
}
