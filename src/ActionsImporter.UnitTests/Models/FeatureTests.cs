using System.Text.Json;
using ActionsImporter.Models;
using NUnit.Framework;

namespace ActionsImporter.UnitTests.Models;

[TestFixture]
public class FeatureTests
{
    private readonly string featureResult = @"
  {
    ""name"": ""actions/cache"",
    ""description"": ""Control usage of actions/cache inside of workflows. Outputs a comment if not enabled."",
    ""enabled"": false,
    ""ghes_version"": ""ghes-3.5"",
    ""customer_facing"": true,
    ""env_name"": ""FEATURE_ACTIONS_CACHE""
  }
    ";

    [Test]
    public void Initialize()
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
        var feature = JsonSerializer.Deserialize<Feature>(featureResult, options);

        Assert.AreEqual("actions/cache", feature?.Name);
        Assert.AreEqual("Control usage of actions/cache inside of workflows. Outputs a comment if not enabled.", feature?.Description);
        Assert.AreEqual("FEATURE_ACTIONS_CACHE", feature?.EnvName);
        Assert.IsFalse(feature?.Enabled);
    }

    [Test]
    public void EnabledMessage()
    {
        var enabledFeature = new Feature
        {
            Enabled = true,
        };
        Assert.AreEqual("enabled", enabledFeature.EnabledMessage());
    }

    [Test]
    public void DisabledMessage()
    {
        var disabledFeature = new Feature
        {
            Enabled = false,
        };
        Assert.AreEqual("disabled", disabledFeature.EnabledMessage());

    }
}
