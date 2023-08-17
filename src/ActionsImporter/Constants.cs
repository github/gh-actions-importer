using ActionsImporter.Models;

namespace ActionsImporter;

public static class Constants
{
    private static readonly List<Variable> UserInputVariables = new()
    {
        new Variable("GITHUB_ACCESS_TOKEN", Provider.GitHub, "Personal access token for GitHub"),
        new Variable("GITHUB_INSTANCE_URL", Provider.GitHub, "Base url of the GitHub instance", "https://github.com"),
        new Variable("AZURE_DEVOPS_ACCESS_TOKEN", Provider.AzureDevOps, "Personal access token for Azure DevOps"),
        new Variable("AZURE_DEVOPS_INSTANCE_URL", Provider.AzureDevOps, "Base url of the Azure DevOps instance", "https://dev.azure.com"),
        new Variable("AZURE_DEVOPS_ORGANIZATION", Provider.AzureDevOps, "Azure DevOps organization name"),
        new Variable("AZURE_DEVOPS_PROJECT", Provider.AzureDevOps, "Azure DevOps project name"),
        new Variable("BAMBOO_ACCESS_TOKEN", Provider.Bamboo, "Personal access token for Bamboo"),
        new Variable("BAMBOO_INSTANCE_URL", Provider.Bamboo, "Base url of the Bamboo instance"),
        new Variable("CIRCLE_CI_ACCESS_TOKEN", Provider.CircleCI, "Personal access token for CircleCI"),
        new Variable("CIRCLE_CI_INSTANCE_URL", Provider.CircleCI, "Base url of the CircleCI instance", "https://circleci.com"),
        new Variable("CIRCLE_CI_ORGANIZATION", Provider.CircleCI, "CircleCI organization name"),
        new Variable("GITLAB_ACCESS_TOKEN", Provider.GitLabCI, "Private token for GitLab"),
        new Variable("GITLAB_INSTANCE_URL", Provider.GitLabCI, "Base url of the GitLab instance", "https://gitlab.com"),
        new Variable("JENKINS_ACCESS_TOKEN", Provider.Jenkins, "Personal access token for Jenkins"),
        new Variable("JENKINS_USERNAME", Provider.Jenkins, "Username of Jenkins user"),
        new Variable("JENKINS_INSTANCE_URL", Provider.Jenkins, "Base url of the Jenkins instance"),
        new Variable("TRAVIS_CI_ACCESS_TOKEN", Provider.TravisCI, "Personal access token for Travis CI"),
        new Variable("TRAVIS_CI_INSTANCE_URL", Provider.TravisCI, "Base url of the Travis CI instance", "https://travis-ci.com"),
        new Variable("TRAVIS_CI_ORGANIZATION", Provider.TravisCI, "Travis CI organization name"),
        new Variable("BITBUCKET_ACCESS_TOKEN", Provider.Bitbucket, "Personal access token for Bitbucket"),

    };

    public static List<string> ProviderNames => UserInputVariables.Where(v => v.Provider != Provider.GitHub).Select(v => v.ProviderName).Distinct().ToList();

    public static List<string> EnvironmentVariables
    {
        get
        {
            var environmentVariables = new List<string>
            {
                "GH_ACCESS_TOKEN", "GH_INSTANCE_URL",
                "YAML_VERBOSITY", "HTTP_PROXY", "HTTPS_PROXY", "NO_PROXY", "OCTOKIT_PROXY", "OCTOKIT_SSL_VERIFY_MODE",
                "INSTALLATION_TYPE"
            };
            environmentVariables.AddRange(UserInputVariables.Select(x => x.Key));

            return environmentVariables;
        }
    }

    public static IEnumerable<Variable> VariablesForProvider(string provider)
        => UserInputVariables.Where(x => x.ProviderName == provider).ToList();
}
