using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using Valet.Handlers;

namespace Valet.Commands;

public class Update : BaseCommand
{
    protected override string Name => "update";
    protected override string Description => "Update to the latest version of Valet";

    private static readonly Option<string> UsernameOption = new(new[] { "--username", "-u" })
    {
        Description = "Username to authenticate with GHCR. Can optionally be set with GHCR_USERNAME env variable.",
        IsRequired = false,
    };

    private static readonly Option<string> PasswordOption = new(new[] { "--password", "-p" })
    {
        Description = "Access token to authenticate with GHCR (requires read:packages scope).  Can optionally be set with GHCR_PASSWORD env variable.",
        IsRequired = false,
    };

    private static readonly Option<bool> PasswordStdInOption = new(new[] { "--password-stdin" })
    {
        Description = "Access token from standard input to authenticate with GHCR (requires read:packages scope).",
        IsRequired = false,
    };

    protected override Command GenerateCommand(App app)
    {
        var command = base.GenerateCommand(app);

        command.AddOption(UsernameOption);
        command.AddOption(PasswordOption);
        command.AddOption(PasswordStdInOption);

        command.Handler = CommandHandler.Create((string? username, string? password, bool passwordStdin) => app.UpdateValetAsync(username, password, passwordStdin));

        return command;
    }
}