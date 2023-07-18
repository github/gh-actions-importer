using System.CommandLine;
using ActionsImporter.Models;

namespace ActionsImporter.Commands;

public static class Common
{

    public static readonly Option<bool> Prerelease = new("--prerelease")
    {
        Description = "Use prerelease image for GitHub Actions Importer",
        IsRequired = false,
    };

    public static readonly Option<bool> Experimental = new("--experimental")
    {
        Description = "Enable experimental and unsupported features.",
        IsRequired = false,
        IsHidden = true
    };

    public static readonly Option<bool> NoHostNetwork = new("--no-host-network")
    {
        Description = "Use docker's default bridge network instead of the host machine's network.",
        IsRequired = false,
    };

    public static Command AppendTransformerOptions(this Command command)
    {
        ArgumentNullException.ThrowIfNull(command);

        command.AddGlobalOption(
            new Option<string[]>(new[] { "--allowed-actions" })
            {
                Description = "An allowed list of GitHub actions to map to.",
                AllowMultipleArgumentsPerToken = true
            }
        );

        command.AddGlobalOption(
            new Option<bool>(new[] { "--allow-verified-actions" })
            {
                Description = "Boolean value to only allow verified actions."
            }
        );

        command.AddGlobalOption(
            new Option<bool>(new[] { "--allow-github-created-actions" })
            {
                Description = "Boolean value allowing only GitHub created actions."
            }
        );

        command.AddGlobalOption(
            new Option<YamlVerbosity>(new[] { "--yaml-verbosity" })
            {
                Description = "YAML verbosity level."
            }
        );

        command.AddGlobalOption(
            new Option<FileInfo[]>(new[] { "--custom-transformers" })
            {
                Description = "Paths to custom transformers.",
                AllowMultipleArgumentsPerToken = true
            }
        );

        command.AddGlobalOption(
            new Option<string>(new[] { "--features" })
            {
                Description = "GHES features to enable in transformed workflows."
            }
        );

        command.AddGlobalOption(
            new Option<string[]>(new[] { "--enable-features" })
            {
                Description = "A list of features to enable by name. Use `gh actions-importer list-features` to see available features.",
                IsRequired = false,
                AllowMultipleArgumentsPerToken = true
            }
        );

        command.AddGlobalOption(
            new Option<string[]>(new[] { "--disable-features" })
            {
                Description = "A list of features to disable by name. Use `gh actions-importer list-features` to see available features.",
                IsRequired = false,
                AllowMultipleArgumentsPerToken = true
            }
        );

        command.AddGlobalOption(
             new Option<FileInfo[]>(new[] { "--github-instance-url" })
             {
                 Description = "The URL of the GitHub instance.",
                 IsRequired = false
             }
         );

        command.AddGlobalOption(
            new Option<string>(new[] { "--github-access-token" })
            {
                Description = "Access token for the GitHub repo.",
                IsRequired = false
            }
        );

        return command;
    }

    public static Command AppendPrereleaseOption(this Command command)
    {
        ArgumentNullException.ThrowIfNull(command);

        command.AddGlobalOption(Prerelease);

        return command;
    }

    public static Command AppendGeneralOptions(this Command command)
    {
        ArgumentNullException.ThrowIfNull(command);

        command.AddGlobalOption(
            new Option<string>(new[] { "--credentials-file" })
            {
                Description = "The file containing the credentials to use."
            }
        );

        command.AddGlobalOption(
            new Option<bool>(new[] { "--no-telemetry" })
            {
                Description = "Boolean value to disallow telemetry."
            }
        );

        command.AddGlobalOption(
            new Option<bool>(new[] { "--no-ssl-verify" })
            {
                Description = "Disable ssl certificate verification."
            }
        );

        command.AddGlobalOption(
            new Option<bool>(new[] { "--no-http-cache" })
            {
                Description = "Disable caching of http responses."
            }
        );

        command.AddGlobalOption(Experimental);

        command.AddGlobalOption(Prerelease);

        command.AddGlobalOption(NoHostNetwork);

        return command;
    }

    public static Command AppendGeneralRequiredOptions(this Command command)
    {
        ArgumentNullException.ThrowIfNull(command);

        command.AddGlobalOption(
            new Option<DirectoryInfo>(new[] { "--output-dir", "-o" })
            {
                IsRequired = true,
                Description = "The location for any output files."
            }
        );

        return command;
    }

    public static void AppendCommonOptions(this Command command)
    {
        command.AppendGeneralRequiredOptions()
               .AppendTransformerOptions()
               .AppendGeneralOptions();
    }
}
