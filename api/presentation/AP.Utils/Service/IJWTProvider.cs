using AP.Data.dtos.responses;
using AP.Data.entites;

namespace AP.Utils.Service;

public interface IJWTProvider
{
    LoginResponse CreateTokenAsync(Users user);
}
