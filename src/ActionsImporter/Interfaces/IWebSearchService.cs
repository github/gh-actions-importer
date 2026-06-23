namespace ActionsImporter.Interfaces;

/// <summary>
/// Service for searching the web via Ollama's web search capability.
/// Used when the LLM needs additional context for workflow conversion.
/// </summary>
public interface IWebSearchService
{
    /// <summary>
    /// Searches the web for information using Ollama's web search.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <returns>Search results as a formatted string.</returns>
    Task<string> SearchAsync(string query);

    /// <summary>
    /// Checks if web search is available.
    /// </summary>
    Task<bool> IsAvailableAsync();
}
