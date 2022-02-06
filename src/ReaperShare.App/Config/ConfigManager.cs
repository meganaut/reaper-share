
public class ConfigManager : IDisposable
{
    private readonly Config _config;
    private readonly string _configPath;
    private readonly DirectoryInfo? _rootDirectoryInfo;
    private readonly GitManager _gitManager;
    
    public ConfigManager(Config config, string configPath)
    {
    
        _config = config;
        _configPath = configPath;
        _rootDirectoryInfo = File.Exists(configPath) ? new FileInfo(configPath).Directory : throw new FileNotFoundException($"Couldnt read config at ${configPath}");
        _gitManager = new GitManager(config.Repository);
    }

    public Config Config => _config;

    public bool EnvironmentUpdated { get; private set; } = false;

    public void Dispose()
    {
        _gitManager.Dispose();
    }

    public async Task ValidateAndUpdateEnvironment()
    {

        // check for a git directory
        if(!_gitManager.DirectoryIsGitRepo())
        {
            throw new Exception("Configured git repository is not a repository");
        }

        // check the state of the git dir
        var status = _gitManager.GetStatus();

        // check state of upstream ? fetch?
        if (status.HasUpstream)
        {
            _gitManager.Update(_config);
        }
        else
        {
            // try to set up the remote

        }
        

        // check for conflicts?

        await Task.Delay(5000);

        EnvironmentUpdated = true;
    }
}

