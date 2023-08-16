namespace ActionsImporter.Models;

public readonly struct Variable
{
    public Variable(string key, Provider provider, string message, string? defaultValue = null)
    {
        Key = key;
        Provider = provider;
        Message = message;
        DefaultValue = defaultValue;
    }

    public string Key { get; }
    public string ProviderName => Provider switch
    {
        Provider.GitHub => "GitHub",
        Provider.AzureDevOps => "Azure DevOps",
        Provider.CircleCI => "CircleCI",
        Provider.GitLabCI => "GitLab CI",
        Provider.Jenkins => "Jenkins",
        Provider.TravisCI => "Travis CI",
        Provider.Bamboo => "Bamboo",
        Provider.Bitbucket => "Bitbucket",
        _ => throw new ArgumentOutOfRangeException()
    };

    public bool IsPassword => Key.EndsWith("ACCESS_TOKEN", StringComparison.Ordinal) || Key.EndsWith("PASSWORD", StringComparison.Ordinal);
    public string Message { get; }
    public string? DefaultValue { get; }

    public string? Placeholder => DefaultValue is not null && IsPassword ? $"({DefaultValue})" : null;

    public Provider Provider { get; }
}
