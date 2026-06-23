using System.Text.Json.Serialization;

namespace ActionsImporter.Models;

/// <summary>
/// Request model for Ollama API generate endpoint.
/// </summary>
public class OllamaGenerateRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = null!;

    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = null!;

    [JsonPropertyName("system")]
    public string? System { get; set; }

    [JsonPropertyName("stream")]
    public bool Stream { get; set; } = false;

    [JsonPropertyName("options")]
    public OllamaOptions? Options { get; set; }
}

/// <summary>
/// Options for Ollama generation.
/// </summary>
public class OllamaOptions
{
    [JsonPropertyName("temperature")]
    public double? Temperature { get; set; } = 0.7;

    [JsonPropertyName("top_k")]
    public int? TopK { get; set; } = 40;

    [JsonPropertyName("top_p")]
    public double? TopP { get; set; } = 0.9;
}

/// <summary>
/// Response model for Ollama API generate endpoint.
/// </summary>
public class OllamaGenerateResponse
{
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("response")]
    public string? Response { get; set; }

    [JsonPropertyName("done")]
    public bool Done { get; set; }

    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    [JsonPropertyName("eval_count")]
    public int? EvalCount { get; set; }

    [JsonPropertyName("eval_duration")]
    public long? EvalDuration { get; set; }

    [JsonPropertyName("prompt_eval_count")]
    public int? PromptEvalCount { get; set; }

    [JsonPropertyName("prompt_eval_duration")]
    public long? PromptEvalDuration { get; set; }
}

/// <summary>
/// Response model for Ollama API tags endpoint (list models).
/// </summary>
public class OllamaTagsResponse
{
    [JsonPropertyName("models")]
    public List<OllamaModel>? Models { get; set; }
}

/// <summary>
/// Model information from Ollama.
/// </summary>
public class OllamaModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("modified_at")]
    public string? ModifiedAt { get; set; }

    [JsonPropertyName("size")]
    public long? Size { get; set; }

    [JsonPropertyName("digest")]
    public string? Digest { get; set; }
}
