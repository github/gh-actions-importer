using System.Collections.Immutable;
using ActionsImporter.Models;

namespace ActionsImporter.Interfaces;

public interface IConfigurationService
{
    Task<ImmutableDictionary<string, string>> ReadCurrentVariablesAsync(string filePath = ".env.local");
    ImmutableDictionary<string, string> GetFeaturesInput(List<Feature> features);
    ImmutableDictionary<string, string> GetUserInput();
    Task WriteVariablesAsync(ImmutableDictionary<string, string> variables, string filePath = ".env.local");

    ImmutableDictionary<string, string> MergeVariables(ImmutableDictionary<string, string> currentVariables, ImmutableDictionary<string, string> newVariables)
    {
        ArgumentNullException.ThrowIfNull(currentVariables);
        ArgumentNullException.ThrowIfNull(newVariables);

        return currentVariables.SetItems(newVariables);
    }
}
