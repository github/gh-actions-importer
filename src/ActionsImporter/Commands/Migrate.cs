using System.CommandLine;

namespace ActionsImporter.Commands;

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

    private static readonly Option<string> WorkflowFilePrefix = new("--workflow-file-prefix")
    {
        Description = "The prefix for the workflow file names.",
        IsRequired = false
    };

    private static readonly Option<string> CommitMessage = new("--commit-message")
    {
        Description = "The commit message to use when committing the workflow file.",
        IsRequired = false
    };

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);
        command.AppendCommonOptions();

        command.AddGlobalOption(TargetUrl);
        command.AddGlobalOption(WorkflowFilePrefix);
        command.AddGlobalOption(CommitMessage);

        command.AddCommand(new AzureDevOps.Migrate(_args).Command(app));
        command.AddCommand(new Bamboo.Migrate(_args).Command(app));
        command.AddCommand(new Bitbucket.Migrate(_args).Command(app));
        command.AddCommand(new Circle.Migrate(_args).Command(app));
        command.AddCommand(new GitLab.Migrate(_args).Command(app));
        command.AddCommand(new Jenkins.Migrate(_args).Command(app));
        command.AddCommand(new Travis.Migrate(_args).Command(app));

        return command;
    }
}
