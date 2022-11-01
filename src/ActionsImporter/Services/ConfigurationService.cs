using System.Collections.Immutable;
using ActionsImporter.Interfaces;
using Sharprompt;

namespace ActionsImporter.Services;

public class ConfigurationService : IConfigurationService
{
    public async Task<ImmutableDictionary<string, string>> ReadCurrentVariablesAsync(string filePath = ".env.local")
    {
        if (!File.Exists(filePath))
            return ImmutableDictionary<string, string>.Empty;

        var lines = await File.ReadAllLinesAsync(filePath);
        var variables = ImmutableDictionary.CreateBuilder<string, string>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var variable = line.Split('=', StringSplitOptions.TrimEntries);
            if (variable.Length != 2 || string.IsNullOrWhiteSpace(variable[1])) continue;

            variables[variable[0]] = variable[1];
        }

        return variables.ToImmutable();
    }

    public ImmutableDictionary<string, string> GetUserInput()
    {
        var providers = Prompt.MultiSelect(
            "Which CI providers are you configuring?",
            new[] { "Azure DevOps", "CircleCI", "GitLab CI", "Jenkins", "Travis CI" },
            pageSize: 5
        );

        var input = ImmutableDictionary.CreateBuilder<string, string>();

        Console.WriteLine("Enter the following values (leave empty to omit):");

        foreach (var provider in providers.Prepend("GitHub"))
        {
            if (string.IsNullOrWhiteSpace(provider)) continue;

            var variables = Constants.VariablesForProvider(provider);

            foreach (var variable in variables)
            {
                string? variableValue;
                if (variable.IsPassword)
                {
                    var value = Prompt.Password(variable.Message, placeholder: variable.Placeholder);
                    variableValue = string.IsNullOrWhiteSpace(value) ? variable.DefaultValue : value;
                }
                else
                {
                    variableValue = Prompt.Input<string>(variable.Message, variable.DefaultValue);
                }

                if (string.IsNullOrWhiteSpace(variableValue)) continue;

                input[variable.Key] = variableValue;
            }
        }

        return input.ToImmutable();
    }

    public async Task WriteVariablesAsync(ImmutableDictionary<string, string> variables, string filePath = ".env.local")
    {
        var lines = variables.Select(kvp => $"{kvp.Key}={kvp.Value}").ToList();
        await File.WriteAllLinesAsync(filePath, lines);
    }
}
