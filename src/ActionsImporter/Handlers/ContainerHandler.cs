namespace ActionsImporter.Handlers;

public class ContainerHandler
{
    private readonly App _app;

    public ContainerHandler(App app)
    {
        _app = app;
    }

    public Func<Task<int>> Run(string[] args) => () => _app.ExecuteActionsImporterAsync(args);
}
