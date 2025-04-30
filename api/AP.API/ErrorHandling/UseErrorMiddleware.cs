namespace AP.API.ErrorHandling
{
    public static class ErrorMiddlewareExtention
    {
        public static void UseErrorMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorMiddleware>();
        }
    }

}
