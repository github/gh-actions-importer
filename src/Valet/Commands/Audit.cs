using System.CommandLine;
namespace Valet.Commands;

public class Audit : BaseCommand
{
    private readonly string[] _args;

    public Audit(string[] args)
    {
        _args = args;
    }

    protected override string Name => "audit";
    protected override string Description => "An audit will output a list of data used in a CI/CD instance.";

    private static readonly Option<string[]> FoldersOption = new(new[] { "-f", "--folders" })
    {
        Description = "Folders to audit in the instance",
        IsRequired = false,
        AllowMultipleArgumentsPerToken = true
    };

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);
        command.AppendCommonOptions();

        command.AddGlobalOption(FoldersOption);
        command.AddCommand(new AzureDevOps.Audit(_args).Command(app));
        command.AddCommand(new Circle.Audit(_args).Command(app));
        command.AddCommand(new GitLab.Audit(_args).Command(app));
        command.AddCommand(new Jenkins.Audit(_args).Command(app));
        command.AddCommand(new Travis.Audit(_args).Command(app));

        return command;
    }
}