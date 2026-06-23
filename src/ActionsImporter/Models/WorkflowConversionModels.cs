namespace ActionsImporter.Models;

/// <summary>
/// Request for workflow conversion from source format to GitHub Actions.
/// </summary>
public class WorkflowConversionRequest
{
    /// <summary>
    /// Source CI/CD platform (e.g., "Jenkins", "GitLab", "Azure DevOps").
    /// </summary>
    public string SourceFormat { get; set; } = null!;

    /// <summary>
    /// Original workflow file content as a string.
    /// </summary>
    public string WorkflowContent { get; set; } = null!;

    /// <summary>
    /// Optional: Filename of the original workflow (e.g., "Jenkinsfile", ".gitlab-ci.yml").
    /// Helps the LLM understand the format better.
    /// </summary>
    public string? SourceFilename { get; set; }

    /// <summary>
    /// Optional: Project name or context. Useful for understanding workflow purpose.
    /// </summary>
    public string? ProjectContext { get; set; }

    /// <summary>
    /// If true, allows web search for conversion strategies when LLM is uncertain.
    /// </summary>
    public bool AllowWebSearch { get; set; } = true;
}

/// <summary>
/// Result of a workflow conversion.
/// </summary>
public class WorkflowConversionResult
{
    /// <summary>
    /// The converted GitHub Actions workflow YAML.
    /// </summary>
    public string GithubActionsWorkflow { get; set; } = null!;

    /// <summary>
    /// Whether the conversion was successful and ready for use.
    /// </summary>
    public bool IsSuccessful { get; set; } = true;

    /// <summary>
    /// Warnings about the conversion (e.g., "Manual steps required for X").
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Metadata about the conversion (e.g., tokens used, web searches performed).
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new();

    /// <summary>
    /// If conversion failed, the error message.
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Configuration for conversion behavior.
/// </summary>
public class ConversionSettings
{
    /// <summary>
    /// Whether to preserve original comments in the workflow.
    /// </summary>
    public bool PreserveComments { get; set; } = true;

    /// <summary>
    /// Only add new comments for workarounds that couldn't be directly converted.
    /// </summary>
    public bool MinimalComments { get; set; } = true;

    /// <summary>
    /// Try to maintain the same variable names and structure as the original.
    /// </summary>
    public bool MirrorConversion { get; set; } = true;

    /// <summary>
    /// Enable web search when LLM can't find a suitable conversion strategy.
    /// </summary>
    public bool EnableWebSearch { get; set; } = true;

    /// <summary>
    /// Maximum number of web searches allowed per conversion.
    /// </summary>
    public int MaxWebSearches { get; set; } = 3;
}
