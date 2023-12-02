using Microsoft.Extensions.Configuration;

namespace RabbitMQDockerEntityExample.Settings;

public class Settings
{
    private readonly IConfiguration _config;

    public Settings(IConfiguration config)
    {
        _config = config;
    }

    public MessagingServiceSettings MessagingServiceSettings => new(_config);
    public ConnectionStrings ConnectionStrings => new(_config);
}

public class MessagingServiceSettings
{

    private readonly IConfiguration _config;

    public MessagingServiceSettings(IConfiguration config)
    {
        _config = config;
    }

    public string? RabbitMQHostname => _config["RabbitMQ:Hostname"];
    public string? RabbitMQUsername => _config["RabbitMQ:Username"];
    public string? RabbitMQPassword => _config["RabbitMQ:Password"];
}

public class ConnectionStrings
{
    private readonly IConfiguration _config;

    public ConnectionStrings(IConfiguration config)
    {
        _config = config;
    }

    public string? ExampleDb => _config.GetConnectionString("ExampleDb");
}