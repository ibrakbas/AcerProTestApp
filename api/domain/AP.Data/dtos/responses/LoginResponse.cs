namespace AP.Data.dtos.responses;


/// <summary> 
///  Login işlemi sonrası token ve kullanıcı bilgilerini içeren DTO
/// </summary>

public sealed record LoginResponse(
   bool loginSuccess, string error, string token, DateTime expire, string username, string refreshToken, DateTime refreshTokenExpire);
