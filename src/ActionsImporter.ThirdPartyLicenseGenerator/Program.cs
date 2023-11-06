using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

Console.WriteLine("Getting licenses of all C# libraries...");

const string DOTNET_LICENSES_FILE_PATH = "./.licenses/licenses.txt";
const string RUBY_LICENSES_DIRECTORY_PATH = "../valet/.licenses/bundler";
const string GITHUB_ACCESS_TOKEN = "GITHUB_ACCESS_TOKEN";
const string OUTPUT_FILE_PATH = "./ThirdPartyNotices.txt";

var _githubAccessToken = Environment.GetEnvironmentVariable(GITHUB_ACCESS_TOKEN) ??
                         throw new Exception($"{GITHUB_ACCESS_TOKEN} is not set");

Console.WriteLine("Fetching all licenses for packages used in C# application...");
var dotnetLicenses = await GetDotNetLicenses(DOTNET_LICENSES_FILE_PATH);

Console.WriteLine("Reading all licenses for gems used in Ruby application...");
var rubyLicenses = GetRubyLicenses(RUBY_LICENSES_DIRECTORY_PATH);

WriteToFile(dotnetLicenses.Union(rubyLicenses));

async Task<List<LicenseInfo>> GetDotNetLicenses(string licensesFilePath)
{
    var licenseInfos = new List<LicenseInfo>();
    var licenseInfo = new LicenseInfo();
    var lines = File.ReadAllLines(licensesFilePath).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
    var index = 0;
    foreach (var line in lines)
    {
        index++;
        var isLastLine = index >= lines.Length;

        if (line.StartsWith("License notice for", StringComparison.OrdinalIgnoreCase) || isLastLine)
        {
            if (!string.IsNullOrEmpty(licenseInfo.Name))
            {
                Console.WriteLine($"Fetching license info for {licenseInfo.Name} ({licenseInfo.RepoUrl})");

                if (licenseInfo.Version is null)
                {
                    WriteError($"  Cannot find a package version for {licenseInfo.Name}");
                }

                if (licenseInfo.RepoUrl is null)
                {
                    WriteError($"  Cannot find a GitHub repo for {licenseInfo.Name}");
                }
                else
                {
                    (licenseInfo.Text, var type) = await DownloadLicenseTextFromGitHub(licenseInfo.RepoUrl);
                    licenseInfo.Type ??= type;
                }

                if (licenseInfo.Type is null)
                {
                    WriteError($"  Cannot find a license type for {licenseInfo.Name}");
                }

                if (licenseInfo.Text is null)
                {
                    WriteError($"  License text cannot be downloaded for {licenseInfo.Name}, please add it manually");
                }

                licenseInfos.Add(licenseInfo);
                licenseInfo = new LicenseInfo();
            }

            (licenseInfo.Name, licenseInfo.Version) = GetPackageNameAndVersion(line);

            continue;
        }

        if (line.StartsWith("https://github.com", StringComparison.OrdinalIgnoreCase))
        {
            licenseInfo.RepoUrl ??= line.Split(" ").First();
            continue;
        }

        if (line.StartsWith("Licensed under", StringComparison.OrdinalIgnoreCase))
        {
            licenseInfo.Type ??= GetLicenseType(line);
        }
    }

    return licenseInfos;
}

async Task<(string? Text, string? Type)> DownloadLicenseTextFromGitHub(string repoUrl)
{
    const string baseUrl = "https://api.github.com";
    var (owner, repo) = GetRepoOwnerAndName(repoUrl);

    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
    httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
    httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ThirdPartyLicenseGenerator", "1.0.0"));
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _githubAccessToken);

    var response = await httpClient.GetAsync(new Uri($"{baseUrl}/repos/{owner}/{repo}/license"));
    if (!response.IsSuccessStatusCode)
    {
        return (null, null);
    }

    var content = await response.Content.ReadAsStringAsync();
    var jNode = JsonNode.Parse(content);
    var text = Encoding.UTF8.GetString(Convert.FromBase64String((string)jNode?["content"]!));
    return (text, (string)jNode?["license"]?["spdx_id"]!);
}

(string Owner, string Repo) GetRepoOwnerAndName(string repoUrl)
{
    var regex = @"https:\/\/github.com\/(.+)\/(.+)";
    var match = Regex.Match(repoUrl, regex);
    var owner = match.Groups[1].Value;
    var repo = match.Groups[2].Value;
    if (repo.EndsWith(".git", StringComparison.OrdinalIgnoreCase))
    {
        repo = repo[..repo.LastIndexOf(".git", StringComparison.OrdinalIgnoreCase)];
    }
    return (owner, repo);
}

(string Name, string Version) GetPackageNameAndVersion(string line)
{
    var regex = @"License notice for (.*) \(v(.+)\)";
    var match = Regex.Match(line, regex);
    return (match.Groups[1].Value, match.Groups[2].Value);
}

string GetLicenseType(string line)
{
    var regex = "Licensed under (.+)";
    var match = Regex.Match(line, regex);
    return match.Groups[1].Value;
}

void WriteError(string message)
{
    Write(message, ConsoleColor.Red);
}

void WriteWarning(string message)
{
    Write(message, ConsoleColor.Yellow);
}

void Write(string message, ConsoleColor color)
{
    var originalFg = Console.ForegroundColor;
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ForegroundColor = originalFg;
}

List<LicenseInfo> GetRubyLicenses(string licensesDirectoryPath)
{
    var licenseInfos = new List<LicenseInfo>();
    foreach (var licenseFilePath in Directory.GetFiles(licensesDirectoryPath, "*.yml"))
    {
        Console.WriteLine($"Reading license info from {licenseFilePath}");
        using var reader = new StringReader(File.ReadAllText(licenseFilePath));
        var yaml = new YamlStream();
        yaml.Load(reader);

        var rootNode = yaml.Documents[0].RootNode;

        licenseInfos.Add(new LicenseInfo
        {
            Name = (rootNode["name"] as YamlScalarNode)?.Value,
            Version = (rootNode["version"] as YamlScalarNode)?.Value,
            RepoUrl = (rootNode["homepage"] as YamlScalarNode)?.Value,
            Type = (rootNode["license"] as YamlScalarNode)?.Value,
            Text = ((rootNode["licenses"] as YamlSequenceNode)?.FirstOrDefault()?["text"] as YamlScalarNode)?.Value
        });
    }

    return licenseInfos;
}

void WriteToFile(IEnumerable<LicenseInfo> licenseInfos)
{
    Console.WriteLine($"Writing license infos to {OUTPUT_FILE_PATH}");

    const string separator = "---------------------------------------------------------";

    using var writer = new StreamWriter(path: OUTPUT_FILE_PATH, append: false);
    writer.WriteLine("NOTICES");
    writer.WriteLine();
    writer.WriteLine("This repository incorporates material as listed below or described in the code.");
    writer.WriteLine();
    writer.WriteLine();
    writer.WriteLine(separator);

    foreach (var licenseInfo in licenseInfos)
    {
        if (licenseInfo.Text is null)
        {
            WriteWarning($"{licenseInfo.Name} skipped, no text.");
            continue;
        }

        writer.WriteLine();
        writer.WriteLine($"{licenseInfo.Name} {licenseInfo.Version} - {licenseInfo.Type}");
        writer.WriteLine(licenseInfo.RepoUrl);
        writer.WriteLine();
        writer.WriteLine(licenseInfo.Text);
        writer.WriteLine();
        writer.WriteLine(separator);
        writer.WriteLine();
        writer.WriteLine(separator);
    }
}

internal class LicenseInfo
{
    public string? Name { get; set; }
    public string? Version { get; set; }
    public string? Type { get; set; }
    public string? RepoUrl { get; set; }
    public string? Text { get; set; }
}
