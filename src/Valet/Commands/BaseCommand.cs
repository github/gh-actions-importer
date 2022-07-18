using System.CommandLine;

namespace Valet.Commands;

public abstract class BaseCommand
{
    public Command Command(App app) => GenerateCommand(app);

    protected virtual Command GenerateCommand(App app)
    {
        return new Command(Name, Description);
    }

    protected abstract string Name { get; }

    protected abstract string Description { get; }
}
