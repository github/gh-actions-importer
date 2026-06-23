using System.Text;
using System.Text.Json;
using ActionsImporter.Interfaces;
using ActionsImporter.Models;

namespace ActionsImporter.Services;

/// <summary>
/// Service for communicating with a local Ollama instance for AI-assisted migrations.
/// Handles health checks, model management, and LLM inference.
/// </summary>
public class OllamaService : IOllamaService
{
    private readonly HttpClient _httpClient;
    private readonly string _ollamaEndpoint;
    private readonly string _defaultModel;
    private readonly JsonSerializerOptions _jsonOptions;

    public OllamaService(HttpClient? httpClient = null, string? endpoint = null, string? model = null)
    {
        _httpClient = httpClient ?? new HttpClient();
        
        _ollamaEndpoint = endpoint ?? Environment.GetEnvironmentVariable("OLLAMA_API_ENDPOINT") ?? "http://localhost:11434";
        _defaultModel = model ?? Environment.GetEnvironmentVariable("OLLAMA_MODEL") ?? "gemma4:e4b";
        
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };

        // Ensure the endpoint doesn't have a trailing slash
        _ollamaEndpoint = _ollamaEndpoint.TrimEnd('/');
    }

    /// <summary>
    /// Checks if the Ollama service is available and responsive.
    /// </summary>
    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_ollamaEndpoint}/api/tags", HttpCompletionOption.ResponseHeadersRead);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Generates a complete response from the LLM for the given prompt.
    /// </summary>
    public async Task<string> GenerateAsync(string prompt, string? systemPrompt = null, string? model = null)
    {
        var requestModel = model ?? _defaultModel;
        var request = new OllamaGenerateRequest
        {
            Model = requestModel,
            Prompt = prompt,
            System = systemPrompt,
            Stream = false
        };

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync($"{_ollamaEndpoint}/api/generate", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var ollamaResponse = JsonSerializer.Deserialize<OllamaGenerateResponse>(responseContent, _jsonOptions);

            return ollamaResponse?.Response ?? string.Empty;
        }
        catch (HttpRequestException e)
        {
            throw new InvalidOperationException($"Failed to communicate with Ollama at {_ollamaEndpoint}. Ensure Ollama is running and accessible.", e);
        }
    }

    /// <summary>
    /// Generates a streaming response from the LLM. Yields chunks as they arrive.
    /// Useful for long-running LLM tasks to provide real-time feedback.
    /// </summary>
    public async IAsyncEnumerable<string> GenerateStreamingAsync(string prompt, string? systemPrompt = null, string? model = null)
    {
        var requestModel = model ?? _defaultModel;
        var request = new OllamaGenerateRequest
        {
            Model = requestModel,
            Prompt = prompt,
            System = systemPrompt,
            Stream = true
        };

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            using var response = await _httpClient.PostAsync($"{_ollamaEndpoint}/api/generate", content, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var ollamaResponse = JsonSerializer.Deserialize<OllamaGenerateResponse>(line, _jsonOptions);
                    if (!string.IsNullOrEmpty(ollamaResponse?.Response))
                    {
                        yield return ollamaResponse.Response;
                    }
                }
                catch (JsonException)
                {
                    // Skip malformed JSON lines
                    continue;
                }
            }
        }
        catch (HttpRequestException e)
        {
            throw new InvalidOperationException($"Failed to communicate with Ollama at {_ollamaEndpoint}. Ensure Ollama is running and accessible.", e);
        }
    }

    /// <summary>
    /// Gets the list of available models from the Ollama instance.
    /// </summary>
    public async Task<List<string>> GetAvailableModelsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_ollamaEndpoint}/api/tags");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var tagsResponse = JsonSerializer.Deserialize<OllamaTagsResponse>(responseContent, _jsonOptions);

            return tagsResponse?.Models?.Select(m => m.Name ?? string.Empty)
                .Where(name => !string.IsNullOrEmpty(name))
                .ToList() ?? new List<string>();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Failed to fetch available models from Ollama at {_ollamaEndpoint}.", e);
        }
    }
}
