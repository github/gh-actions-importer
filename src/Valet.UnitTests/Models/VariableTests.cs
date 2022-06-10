using NUnit.Framework;
using Valet.Models;

namespace Valet.UnitTests.Models;

[TestFixture]
public class VariableTests
{
    [TestCase(Provider.GitHub, "GitHub")]
    [TestCase(Provider.AzureDevOps, "Azure DevOps")]
    [TestCase(Provider.CircleCI, "CircleCI")]
    [TestCase(Provider.GitLabCI, "GitLab CI")]
    [TestCase(Provider.Jenkins, "Jenkins")]
    [TestCase(Provider.TravisCI, "Travis CI")]
    public void ProviderName_ValidName_ReturnsExpected(Provider provider, string providerName)
    {
        // Arrange
        var variable = new Variable("FOO", provider, "");

        // Act
        Assert.AreEqual(providerName, variable.ProviderName);
    }

    [TestCase("USERNAME", false)]
    [TestCase("PERSONAL_ACCESS_TOKEN", true)]
    public void IsPassword_ReturnsExpected(string key, bool isPassword)
    {
        // Arrange
        var variable = new Variable(key, Provider.GitHub, "");

        // Act
        Assert.AreEqual(isPassword, variable.IsPassword);
    }

    [TestCase("Personal access token for GitHub", null, "Personal access token for GitHub")]
    [TestCase("Base url of the GitHub instance", "https://github.com", "Base url of the GitHub instance (https://github.com)")]
    public void Message_ReturnsExpected(string helpText, string? defaultValue, string expectedMessage)
    {
        // Arrange
        var variable = new Variable("FOO", Provider.GitHub, helpText, defaultValue);

        // Act
        Assert.AreEqual(expectedMessage, variable.Message);
    }
}