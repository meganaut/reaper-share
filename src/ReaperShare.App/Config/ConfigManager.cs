
public class ConfigManager
{
    private readonly Config _config;
    private readonly string _configPath;
    private readonly DirectoryInfo _rootDirectoryInfo;
    
    public ConfigManager(Config config, string configPath)
    {
    
        _config = config;
        _configPath = configPath;
#pragma warning disable CS8601 // Possible null reference assignment.
        _rootDirectoryInfo = File.Exists(configPath) ? new FileInfo(configPath).Directory : throw new FileNotFoundException($"Couldnt read config at ${configPath}")!;
#pragma warning restore CS8601 // Possible null reference assignment.

    }

    public Config Config => _config;

    public bool EnvironmentUpdated { get; private set; } = false;

    public async Task ValidateAndUpdateEnvironment()
    {
        await Task.Delay(5000);

        EnvironmentUpdated = true;
    }
}

