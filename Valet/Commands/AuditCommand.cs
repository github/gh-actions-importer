using System.CommandLine;

namespace Valet.Commands;

public class AuditCommand : BaseCommand
{
    private readonly string[] _args;
    
    public AuditCommand(string[] args)
    {
        _args = args;
    }
    
    protected override string Name => "audit";
    protected override string Description => "An audit will output a list of data used in a CI/CD instance.";

    private static readonly Option<string[]> FoldersOption = new Option<string[]>(new[] { "-f", "--folders" })
    {
        Description = "Folders to audit in the instance",
        IsRequired = false
    };
    
    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);
    
        command.AddGlobalOption(FoldersOption);
        command.AddCommand(new Audit.AzureDevOpsCommand(_args).Command(app));
        
        return command;
    }
}