namespace Valet.Handlers;

public class UpdateHandler
{
    private readonly App _app;

    public UpdateHandler(App app)
    {
        _app = app;
    }

    public Func<Task<int>> Run() => () => _app.UpdateValetAsync();
}