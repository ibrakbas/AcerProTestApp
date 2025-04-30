namespace AP.Data.dtos.responses;

public sealed record LoginResponse(
   bool loginSuccess, string error, string token, DateTime expire, string username, string refreshToken, DateTime refreshTokenExpire);
