using Microsoft.Extensions.Configuration;

public static class ConfigFactory
{
    const string ConfigDefaultPath = "appsettings.json";
    public static Config Build()
    {
        var builder = new ConfigurationBuilder().Add(source =>
        {
            source.
        })
            .AddJsonFile(ConfigDefaultPath, )
        .

        return new Config();
    }
}