namespace ActionsImporter.Interfaces;

/// <summary>
/// Service for communicating with a local Ollama instance to invoke LLM for AI-assisted migration.
/// </summary>
public interface IOllamaService
{
    /// <summary>
    /// Checks if the Ollama service is available and responsive.
    /// </summary>
    /// <returns>True if Ollama is reachable, false otherwise.</returns>
    Task<bool> IsAvailableAsync();

    /// <summary>
    /// Generates a response from the LLM for the given prompt.
    /// </summary>
    /// <param name="prompt">The prompt to send to the LLM.</param>
    /// <param name="systemPrompt">Optional system prompt to set the context.</param>
    /// <param name="model">The model to use (defaults to environment variable OLLAMA_MODEL).</param>
    /// <returns>The LLM response text.</returns>
    Task<string> GenerateAsync(string prompt, string? systemPrompt = null, string? model = null);

    /// <summary>
    /// Generates a streaming response from the LLM. Yields chunks of the response as they arrive.
    /// </summary>
    /// <param name="prompt">The prompt to send to the LLM.</param>
    /// <param name="systemPrompt">Optional system prompt to set the context.</param>
    /// <param name="model">The model to use (defaults to environment variable OLLAMA_MODEL).</param>
    /// <returns>An async enumerable of response chunks.</returns>
    IAsyncEnumerable<string> GenerateStreamingAsync(string prompt, string? systemPrompt = null, string? model = null);

    /// <summary>
    /// Gets the list of available models from the Ollama instance.
    /// </summary>
    /// <returns>A list of model names available on the Ollama instance.</returns>
    Task<List<string>> GetAvailableModelsAsync();
}
