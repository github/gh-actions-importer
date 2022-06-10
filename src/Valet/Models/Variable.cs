namespace Valet.Models;

public readonly struct Variable
{
    public Variable(string key, Provider provider, string helpText, string? defaultValue = null)
    {
        Key = key;
        Provider = provider;
        HelpText = helpText;
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
        _ => throw new ArgumentOutOfRangeException()
    };

    public bool IsPassword => Key.EndsWith("ACCESS_TOKEN");
    public string HelpText { get; }
    public string? DefaultValue { get; }

    public string Message => DefaultValue is null ? HelpText : $"{HelpText} ({DefaultValue})";

    private Provider Provider { get; }
}