namespace AP.Utils.Auth;


/// <summary> 
///     JWT için geçerli ayarları tutan sınıf. Değerler appsettings.json dosyasından okunur.
/// </summary>
public sealed class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }

}
