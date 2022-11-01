using System.Collections.Immutable;

namespace ActionsImporter.Interfaces;

public interface IConfigurationService
{
    Task<ImmutableDictionary<string, string>> ReadCurrentVariablesAsync(string filePath = ".env.local");
    ImmutableDictionary<string, string> GetUserInput();
    Task WriteVariablesAsync(ImmutableDictionary<string, string> variables, string filePath = ".env.local");

    ImmutableDictionary<string, string> MergeVariables(ImmutableDictionary<string, string> currentVariables, ImmutableDictionary<string, string> newVariables)
    {
        ArgumentNullException.ThrowIfNull(currentVariables);
        ArgumentNullException.ThrowIfNull(newVariables);

        return currentVariables.SetItems(newVariables);
    }
}
