using System.Text.Json.Serialization;

namespace ActionsImporter.Models.Docker;

public class Manifest
{
    [JsonPropertyName("schemaVersion")]
    public int SchemaVersion { get; set; }

    [JsonPropertyName("mediaType")]
    public string? MediaType { get; set; }

    [JsonPropertyName("config")]
    public ManifestConfig? Config { get; set; }

    [JsonPropertyName("layers")]
    public List<ManifestConfig>? Layers { get; set; }

    public string? GetDigest()
    {
        return Config?.Digest?.Split(':').ElementAtOrDefault(1)?.Trim();
    }
}
