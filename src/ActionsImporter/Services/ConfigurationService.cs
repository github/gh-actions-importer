using System.Collections.Immutable;
using ActionsImporter.Interfaces;
using ActionsImporter.Models;
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

    public ImmutableDictionary<string, string> GetUserInput(List<Feature> features)
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


        // In case we didn't find any features, skip this step to let the customer continue configuration
        if (features != null &&
            features.Count > 0 &&
            Prompt.Confirm("Configure optional features? For more information, run 'gh actions-importer list-features'"))
        {
            var featureIndices = Prompt.MultiSelect("Which features would you like to configure?", Enumerable.Range(0, features.Count).ToArray(), textSelector: i => features[i].Name);

            foreach (var index in featureIndices)
            {
                var feature = features[index];
                var choice = Prompt.Select($"{feature.Name} ({feature.EnabledMessage()})", new[] { true, false }, textSelector: x => x ? "Enable" : "Disable");

                if (choice != feature.Enabled)
                {
                    input[feature.EnvName] = choice ? "true" : "false";
                }
            }
        }


        return input.ToImmutable();
    }

    public async Task WriteVariablesAsync(ImmutableDictionary<string, string> variables, string filePath = ".env.local")
    {
        var lines = variables.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key}={kvp.Value}").ToList();
        await File.WriteAllLinesAsync(filePath, lines);
    }
}
