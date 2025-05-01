namespace AP.API.ErrorHandling;


/// <summary> 
///    Hata yakalama middleware'ı  extension sınıfı
/// </summary>
public static class ErrorMiddlewareExtention
{
    public static void UseErrorMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorMiddleware>();
    }
}
