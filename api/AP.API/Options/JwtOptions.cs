using Microsoft.Extensions.Options;


/// <summary> 
///    JWT için geçerli ayarları tutan sınıf. Değerler appsettings.json dosyasından okunur.
/// </summary>
namespace AP.API.Options;

public sealed class JwtOptions : IConfigureOptions<AP.Utils.Auth.JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(AP.Utils.Auth.JwtOptions options)
    {
        _configuration.GetSection("JWT").Bind(options);
    }
}
