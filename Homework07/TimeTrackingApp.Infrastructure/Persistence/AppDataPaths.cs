namespace TimeTrackingApp.Infrastructure.Persistence;

public class AppDataPaths
{
    private readonly string _rootPath;

    public AppDataPaths(string? rootPath = null)
    {
        _rootPath = rootPath ?? Path.Combine(AppContext.BaseDirectory, "AppData");
    }

    public string UsersFilePath => Path.Combine(_rootPath, "users.json");
    public string ActivitiesFilePath => Path.Combine(_rootPath, "activities.json");
}
