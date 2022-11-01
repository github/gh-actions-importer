using System.Text.Json.Serialization;

namespace ActionsImporter.Models.Docker;

public class ManifestConfig
{
    [JsonPropertyName("mediaType")]
    public string? MediaType { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("digest")]
    public string? Digest { get; set; }
}
