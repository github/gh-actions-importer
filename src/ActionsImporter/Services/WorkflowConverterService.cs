using ActionsImporter.Interfaces;
using ActionsImporter.Models;

namespace ActionsImporter.Services;

/// <summary>
/// Service for converting CI/CD workflows from various formats to GitHub Actions.
/// Uses an LLM (via Ollama) for intelligent conversion with optional web search for complex cases.
/// </summary>
public class WorkflowConverterService : IWorkflowConverterService
{
    private readonly IOllamaService _ollamaService;
    private readonly IWebSearchService _webSearchService;
    private readonly ConversionSettings _defaultSettings;

    private readonly Dictionary<string, string> _supportedFormats = new()
    {
        { "Jenkins", "Jenkinsfile / Jenkins Pipeline" },
        { "GitLab", "GitLab CI (.gitlab-ci.yml)" },
        { "Azure DevOps", "Azure DevOps YAML Pipelines" },
        { "CircleCI", "CircleCI config.yml" },
        { "Travis CI", ".travis.yml" },
        { "Bamboo", "Bamboo YAML specifications" },
        { "Bitbucket", "Bitbucket Pipelines" }
    };

    public WorkflowConverterService(IOllamaService ollamaService, IWebSearchService webSearchService)
    {
        _ollamaService = ollamaService ?? throw new ArgumentNullException(nameof(ollamaService));
        _webSearchService = webSearchService ?? throw new ArgumentNullException(nameof(webSearchService));
        _defaultSettings = new ConversionSettings();
    }

    public async Task<bool> IsAvailableAsync()
    {
        return await _ollamaService.IsAvailableAsync();
    }

    public async Task<List<string>> GetSupportedFormatsAsync()
    {
        return _supportedFormats.Keys.ToList();
    }

    /// <summary>
    /// Converts a workflow from source format to GitHub Actions YAML.
    /// </summary>
    public async Task<WorkflowConversionResult> ConvertAsync(WorkflowConversionRequest request, ConversionSettings? settings = null)
    {
        settings ??= _defaultSettings;

        try
        {
            var systemPrompt = BuildSystemPrompt(request, settings);
            var userPrompt = BuildUserPrompt(request, settings);

            // First attempt: Use LLM to convert
            var initialConversion = await _ollamaService.GenerateAsync(userPrompt, systemPrompt);

            // Check if conversion looks complete
            if (IsCompleteConversion(initialConversion))
            {
                return new WorkflowConversionResult
                {
                    GithubActionsWorkflow = initialConversion,
                    IsSuccessful = true,
                    Metadata = new Dictionary<string, string>
                    {
                        { "converted_by", "llm" },
                        { "web_searches", "0" }
                    }
                };
            }

            // Second attempt: If conversion seems incomplete, try web search
            if (settings.EnableWebSearch)
            {
                var webSearchPrompt = BuildWebSearchQuery(request, initialConversion);
                var searchResults = await _webSearchService.SearchAsync(webSearchPrompt);

                // Refine conversion with web search results
                var refinedPrompt = $"{userPrompt}\n\nWeb search results for additional context:\n{searchResults}";
                var refinedConversion = await _ollamaService.GenerateAsync(refinedPrompt, systemPrompt);

                return new WorkflowConversionResult
                {
                    GithubActionsWorkflow = refinedConversion,
                    IsSuccessful = true,
                    Warnings = new List<string> { "Conversion used web search for additional context" },
                    Metadata = new Dictionary<string, string>
                    {
                        { "converted_by", "llm_with_web_search" },
                        { "web_searches", "1" }
                    }
                };
            }

            // If we got here, conversion might be partial
            return new WorkflowConversionResult
            {
                GithubActionsWorkflow = initialConversion,
                IsSuccessful = !string.IsNullOrWhiteSpace(initialConversion),
                Warnings = new List<string> { "Conversion may be incomplete; review carefully" },
                Metadata = new Dictionary<string, string>
                {
                    { "converted_by", "llm" },
                    { "web_searches", "0" }
                }
            };
        }
        catch (Exception ex)
        {
            return new WorkflowConversionResult
            {
                IsSuccessful = false,
                ErrorMessage = ex.Message,
                Metadata = new Dictionary<string, string>
                {
                    { "error_type", ex.GetType().Name }
                }
            };
        }
    }

    /// <summary>
    /// Streams the conversion process.
    /// </summary>
    public async IAsyncEnumerable<string> ConvertStreamingAsync(WorkflowConversionRequest request, ConversionSettings? settings = null)
    {
        settings ??= _defaultSettings;

        var systemPrompt = BuildSystemPrompt(request, settings);
        var userPrompt = BuildUserPrompt(request, settings);

        try
        {
            await foreach (var chunk in _ollamaService.GenerateStreamingAsync(userPrompt, systemPrompt))
            {
                yield return chunk;
            }
        }
        catch (Exception ex)
        {
            yield return $"\n\nError during conversion: {ex.Message}";
        }
    }

    private string BuildSystemPrompt(WorkflowConversionRequest request, ConversionSettings settings)
    {
        var prompt = $@"You are an expert CI/CD pipeline converter specializing in migrations from {request.SourceFormat} to GitHub Actions.

Your task is to convert CI/CD workflows to GitHub Actions YAML format with the following rules:

1. **Mirror Conversion**: Preserve the original structure and variable names where possible.
2. **Direct Equivalents**: When a {request.SourceFormat} feature has a direct GitHub Actions equivalent, use it.
3. **Workarounds**: When no direct equivalent exists, redefine the functionality to maintain the original workflow's intent.
4. **Comments**: {(settings.PreserveComments ? "Preserve all original comments." : "Remove original comments.")} Only add new comments if a workaround was implemented that needs clarification.
5. **YAML Quality**: Output valid, production-ready GitHub Actions YAML.
6. **Minimal Changes**: Make minimal changes necessary—don't refactor unless required for GitHub Actions compatibility.

Output ONLY the valid GitHub Actions workflow YAML, with no additional text, explanations, or markdown formatting.";

        return prompt;
    }

    private string BuildUserPrompt(WorkflowConversionRequest request, ConversionSettings settings)
    {
        var prompt = $@"Convert this {request.SourceFormat} workflow to GitHub Actions:

";

        if (!string.IsNullOrEmpty(request.SourceFilename))
            prompt += $"Source file: {request.SourceFilename}\n";

        if (!string.IsNullOrEmpty(request.ProjectContext))
            prompt += $"Project context: {request.ProjectContext}\n";

        prompt += $@"
--- BEGIN {request.SourceFormat.ToUpper()} WORKFLOW ---
{request.WorkflowContent}
--- END {request.SourceFormat.ToUpper()} WORKFLOW ---

Convert to GitHub Actions YAML format:";

        return prompt;
    }

    private string BuildWebSearchQuery(WorkflowConversionRequest request, string partialConversion)
    {
        // Extract features that might need web search clarification
        var features = ExtractUncertainFeatures(partialConversion);
        
        if (features.Any())
        {
            return $"How to implement {string.Join(", ", features)} in GitHub Actions";
        }

        return $"GitHub Actions equivalent of {request.SourceFormat} CI/CD features";
    }

    private bool IsCompleteConversion(string conversion)
    {
        if (string.IsNullOrWhiteSpace(conversion))
            return false;

        // Check for basic GitHub Actions structure
        var hasWorkflowName = conversion.Contains("name:") || conversion.Contains("name :");
        var hasOn = conversion.Contains("on:") || conversion.Contains("on :");
        var hasJobs = conversion.Contains("jobs:") || conversion.Contains("jobs :");

        return hasWorkflowName && hasOn && hasJobs;
    }

    private List<string> ExtractUncertainFeatures(string conversion)
    {
        var uncertain = new List<string>();

        // Look for markers that might indicate uncertain conversions
        if (conversion.Contains("TODO") || conversion.Contains("FIXME") || conversion.Contains("NOTE"))
        {
            uncertain.Add("uncertain_conversion_markers");
        }

        if (conversion.Contains("run:") && conversion.Contains("// convert") || conversion.Contains("# convert"))
        {
            uncertain.Add("manual_step_indicators");
        }

        return uncertain;
    }
}
