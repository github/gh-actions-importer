
using System.Runtime.InteropServices;
using ActionsImporter.Interfaces;

namespace ActionsImporter.Services;

public class RuntimeService : IRuntimeService
{
    public bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
}
