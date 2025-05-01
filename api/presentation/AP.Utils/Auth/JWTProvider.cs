using AP.Data.dtos.responses;
using AP.Data.entites;
using AP.Utils.Service;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AP.Utils.Auth;


/// <summary> 
///     JWT yönetimini içeren sınıf. JWT ayarlarını ve token oluşturma işlemlerini içerir.
///     JWT Expiry süresi 1 saat olarak ayarlanmıştır.
/// </summary>
public sealed class JWTProvider : IJWTProvider
{
    private readonly JwtOptions options;

    public JWTProvider(IOptions<JwtOptions> options)
    {
        this.options = options.Value;
    }

    public LoginResponse CreateTokenAsync(Users user)
    {
        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
           new Claim(ClaimTypes.Role,(bool)user.AP_UserRole.IsAdmin ? "Admin" : "User"),
        
        };
        JwtSecurityToken token = new(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            notBefore: DateTime.Now,
             expires: DateTime.Now.AddHours(1),
              signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                  new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                  Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256)
              );
        string _token = new JwtSecurityTokenHandler().WriteToken(token);

        string _refreshToken = Guid.NewGuid().ToString();
        DateTime _refreshTokenExpiryTime = DateTime.Now.AddMinutes(15);

        return new   LoginResponse( true,
            "", _token, token.ValidTo, token.Claims.First().Value
             ,_refreshToken, _refreshTokenExpiryTime);
    }
}
