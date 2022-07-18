using System.CommandLine;
namespace Valet.Commands;

public class Migrate : BaseCommand
{
    private readonly string[] _args;

    public Migrate(string[] args)
    {
        _args = args;
    }

    protected override string Name => "migrate";
    protected override string Description => "Convert a pipeline to a GitHub Actions workflow and open a pull request with the changes.";

    private static readonly Option<string> TargetUrl = new("--target-url")
    {
        Description = "The URL of the GitHub repo to migrate into.",
        IsRequired = true
    };

    private static readonly Option<string> GitHubInstanceUrl = new("--github-instance-url")
    {
        Description = "The URL of the GitHub instance.",
        IsRequired = false
    };

    private static readonly Option<string> GitHubAccessToken = new("--github-access-token")
    {
        Description = "Access token for the GitHub repo to migrate to.",
        IsRequired = false
    };

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);
        command.AppendCommonOptions();

        command.AddGlobalOption(TargetUrl);
        command.AddGlobalOption(GitHubInstanceUrl);
        command.AddGlobalOption(GitHubAccessToken);

        command.AddCommand(new AzureDevOps.Migrate(_args).Command(app));
        command.AddCommand(new Circle.Migrate(_args).Command(app));
        command.AddCommand(new GitLab.Migrate(_args).Command(app));
        command.AddCommand(new Jenkins.Migrate(_args).Command(app));
        command.AddCommand(new Travis.Migrate(_args).Command(app));

        return command;
    }
}
