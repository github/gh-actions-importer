using System.Text;
using System.Text.Json;
using ActionsImporter.Interfaces;

namespace ActionsImporter.Services;

/// <summary>
/// Service for performing web searches via Ollama's web search API.
/// Used when the LLM needs additional context for workflow conversion.
/// </summary>
public class WebSearchService : IWebSearchService
{
    private readonly HttpClient _httpClient;
    private readonly string _ollamaEndpoint;
    private readonly string _model;
    private readonly JsonSerializerOptions _jsonOptions;

    public WebSearchService(HttpClient? httpClient = null, string? endpoint = null, string? model = null)
    {
        _httpClient = httpClient ?? new HttpClient();
        _ollamaEndpoint = endpoint ?? Environment.GetEnvironmentVariable("OLLAMA_API_ENDPOINT") ?? "http://localhost:11434";
        _model = model ?? Environment.GetEnvironmentVariable("OLLAMA_MODEL") ?? "gemma4:e4b";

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };

        _ollamaEndpoint = _ollamaEndpoint.TrimEnd('/');
    }

    /// <summary>
    /// Searches the web using Ollama's web search capability.
    /// </summary>
    public async Task<string> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return string.Empty;

        try
        {
            // Ollama's web search is performed through the generate endpoint with a special prompt
            var searchPrompt = $"Search the web and provide information about: {query}\n\nProvide relevant and concise search results.";

            var request = new
            {
                model = _model,
                prompt = searchPrompt,
                stream = false,
                tools = new[] { "web_search" }  // Enable web search tool
            };

            var json = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_ollamaEndpoint}/api/generate", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Parse the response to extract the search results
            using var doc = JsonDocument.Parse(responseContent);
            if (doc.RootElement.TryGetProperty("response", out var responseText))
            {
                return responseText.GetString() ?? string.Empty;
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Web search failed: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Checks if web search is available in the Ollama instance.
    /// </summary>
    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            // Try a simple search query to verify availability
            var result = await SearchAsync("test query");
            return !string.IsNullOrEmpty(result);
        }
        catch
        {
            return false;
        }
    }
}
