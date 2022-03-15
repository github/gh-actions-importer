using Docker.DotNet.Models;

namespace Valet.Models;

public class Progress : IProgress<JSONMessage>
{
    public void Report(JSONMessage value)
    {
        Console.WriteLine(string.IsNullOrEmpty(value.ErrorMessage)
            ? $"{value.ID} {value.Status} {value.ProgressMessage}"
            : value.ErrorMessage
        );
    }
}