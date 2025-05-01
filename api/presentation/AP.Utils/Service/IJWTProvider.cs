using AP.Data.dtos.responses;
using AP.Data.entites;

namespace AP.Utils.Service;


/// <summary> 
///     JWT yönetimini sağlayan interface
/// </summary>
public interface IJWTProvider
{
    LoginResponse CreateTokenAsync(Users user);
}
