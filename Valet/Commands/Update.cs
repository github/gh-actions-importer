using System.CommandLine;
using Valet.Handlers;

namespace Valet.Commands;

public class Update : BaseCommand
{
    protected override string Name => "update";
    protected override string Description => "Update to the latest version of Valet";

    private static readonly Option<string> UsernameOption = new(new[] { "-u", "--username" })
    {
        Description = "Username to authenticate with GHCR. Can optionally be set with GHCR_USERNAME env variable.",
        IsRequired = false,
    };

    private static readonly Option<string> PasswordOption = new(new[] { "-p", "--password" })
    {
        Description = "Access token to authenticate with GHCR (requires read:packages scope).  Can optionally be set with GHCR_PASSWORD env variable.",
        IsRequired = false,
    };

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);

        command.AddOption(UsernameOption);
        command.AddOption(PasswordOption);

        command.SetHandler(new UpdateHandler(app).Run());

        return command;
    }
}