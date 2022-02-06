using Microsoft.Extensions.Configuration;
using System.Reflection;
public static class ConfigFactory
{
    const string ConfigRelativePath = "./appsettings.json";
    public static ConfigManager Build()
    {
        // get the absolute path to the config file.
        // this file's location is used to determine the working directory, assumed to be a git repository
        var configPath = new Uri(new Uri(Assembly.GetExecutingAssembly().Location), ConfigRelativePath).AbsolutePath;
        var configRoot = new ConfigurationBuilder()
            .AddJsonFile(configPath, false, true)
            .Build();

        var config = configRoot.GetSection(nameof(Config)).Get<Config>()!;

        return new ConfigManager(
            config,
            configPath
            );
    }
}