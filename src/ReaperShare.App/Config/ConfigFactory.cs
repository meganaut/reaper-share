using Microsoft.Extensions.Configuration;
using System.Reflection;
using CommandLine;
public static class ConfigFactory
{
    const string ConfigRelativePath = "./appsettings.json";
    public static ConfigManager Build()
    {
        // get the absolute path to the config file.
        // this file's location is used to determine the working directory, assumed to be a git repository
        var configPath = new Uri(new Uri(Assembly.GetExecutingAssembly().Location), ConfigRelativePath).AbsolutePath;
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile(configPath, false, true);

        if(Environment.GetCommandLineArgs().Any())
        {
            Parser.Default
                .ParseArguments<TestConfigParameter>(Environment.GetCommandLineArgs())
                .WithParsed<TestConfigParameter>(o =>
                {
                    if (!string.IsNullOrEmpty(o.TestConfig))
                    {
                        builder.AddJsonFile(o.TestConfig, true, true);
                    }
                });
        }

        var configRoot = builder.Build();

        var config = configRoot.GetSection(nameof(Config)).Get<Config>()!;

        return new ConfigManager(
            config,
            configPath
            );
    }

    public class TestConfigParameter
    {
        [Option('t', "testConfig", Required = false)]
        public string? TestConfig { get; set; }
    }
}