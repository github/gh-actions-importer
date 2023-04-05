using System.Text.Json.Serialization;

namespace ActionsImporter.Models;

public class Feature
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("env_name")]
    public string EnvName { get; set; } = string.Empty;

    public bool Enabled { get; set; }

    public string EnabledMessage() => Enabled ? "enabled" : "disabled";
}
