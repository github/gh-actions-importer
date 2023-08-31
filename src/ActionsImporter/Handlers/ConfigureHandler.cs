namespace ActionsImporter.Handlers;

public class ConfigureHandler
{
    private readonly App _app;

    public ConfigureHandler(App app)
    {
        _app = app;
    }

    public Func<Task<int>> Run(string[] args) => () => _app.ConfigureAsync(args);
}
