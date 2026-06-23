using ActionsImporter.Models;

namespace ActionsImporter.Interfaces;

/// <summary>
/// Service for converting CI/CD workflows from various formats to GitHub Actions.
/// Uses an LLM to handle the conversion, with optional web search for complex cases.
/// </summary>
public interface IWorkflowConverterService
{
    /// <summary>
    /// Converts a workflow from a source format to GitHub Actions YAML.
    /// </summary>
    /// <param name="request">The conversion request with source workflow and format.</param>
    /// <param name="settings">Optional conversion settings (preserveComments, webSearch, etc.).</param>
    /// <returns>The converted GitHub Actions workflow and metadata.</returns>
    Task<WorkflowConversionResult> ConvertAsync(WorkflowConversionRequest request, ConversionSettings? settings = null);

    /// <summary>
    /// Streams the conversion process, yielding chunks of the converted workflow as they're generated.
    /// Useful for showing real-time progress during long conversions.
    /// </summary>
    /// <param name="request">The conversion request.</param>
    /// <param name="settings">Optional conversion settings.</param>
    /// <returns>An async enumerable of workflow chunks as they're generated.</returns>
    IAsyncEnumerable<string> ConvertStreamingAsync(WorkflowConversionRequest request, ConversionSettings? settings = null);

    /// <summary>
    /// Gets supported source formats that can be converted.
    /// </summary>
    Task<List<string>> GetSupportedFormatsAsync();

    /// <summary>
    /// Checks if the conversion service is available (LLM and Ollama accessible).
    /// </summary>
    Task<bool> IsAvailableAsync();
}
