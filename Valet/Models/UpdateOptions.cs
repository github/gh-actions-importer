using CommandLine;

namespace Valet.Models;

[Verb(name: "update", HelpText = "Update to the latest version of Valet")]
public class UpdateOptions
{
    [Option(
        'u',
        "username", 
        Default = null,
        HelpText = "Username to authenticate with GHCR. Can optionally be set with GHCR_USERNAME env variable.")]
    public string? Username { get; set; }
    
    [Option(
        'p',
        "password", 
        Default = null,
        HelpText = "Access token to authenticate with GHCR (requires read:packages scope).  Can optionally be set with GHCR_PASSWORD env variable.")]
    public string? Password { get; set; }
}