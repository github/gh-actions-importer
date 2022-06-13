using System.CommandLine;
using Valet.Models;

namespace Valet.Commands;

public static class Common
{
    public static Command AppendCommonOptions(Command command)
    {
        command.AddGlobalOption(
            new Option<DirectoryInfo>(new[] { "--output-dir", "-o" })
            {
                IsRequired = true,
                Description = "The location for any output files."
            }
        );

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

        // TODO: Add in enum values
        command.AddGlobalOption(
            new Option<string>(new[] { "--features" })
            {
                Description = "Features to enable in transformed workflows."
            }
        );

        return command;
    }
}