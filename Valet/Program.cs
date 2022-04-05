using System.CommandLine;
using Valet;
using Valet.Commands;
using Valet.Services;

var processService = new ProcessService();

var app = new App(
    new DockerService(processService),
    new AuthenticationService()
);

var command = new RootCommand
{
    new Update().Command(app),
    new Audit(args).Command(app),
    new DryRun(args).Command(app),
    new Migrate(args).Command(app),
    new Forecast(args).Command(app)
};

command.AddGlobalOption(
    new Option<DirectoryInfo>(new[] { "--output-dir", "-o" })
    {
        IsRequired = true,
        Description = "The location for any output files."
    }
);

// TODO: Add global options to command
// class_option :allowed_actions,              type: :array,   desc: "An allowed list of GitHub actions to map to."
// class_option :allow_verified_actions,       type: :boolean, desc: "Boolean value to only allow verified actions."
// class_option :allow_github_created_actions, type: :boolean, desc: "Boolean value allowing only GitHub created actions."
// class_option :yaml_verbosity,               type: :string,  desc: "YAML verbosity level.", enum: [Valet::YamlVerbosity::QUIET, Valet::YamlVerbosity::MINIMAL, Valet::YamlVerbosity::INFO]
// class_option :custom_transformers,          type: :array,   desc: "Paths to custom transformers."
// class_option :output_dir, aliases: :o,      type: :string,  desc: "The location for any output files."
// class_option :credentials_file,             type: :string,  desc: "The file containing the credentials to use."
// class_option :no_telemetry,                 type: :boolean, desc: "Boolean value to disallow telemetry."
// class_option :ssl_verify,                   type: :boolean, default: true, desc: "Verify ssl certificates."
// class_option :features,                     type: :string,  desc: "Features to enable in transformed workflows.", enum: Features::VERSION_MAP.keys, default: "all"

await command.InvokeAsync(args);
