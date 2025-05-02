namespace AP.UI.Services;

public interface IApiConnectionService
{
    string GetApiBaseUrl();
}

public class ApiConnectionService : IApiConnectionService
{
    private readonly IConfiguration _configuration;

    public ApiConnectionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetApiBaseUrl()
    {
        return _configuration["ApiSettings:BaseUrl"];
    }
}
