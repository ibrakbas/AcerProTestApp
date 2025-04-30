using AP.Data.entites.helperEntities;
using AP.Generic.Services;
using System.Net;

namespace AP.API.ErrorHandling;


public sealed class ErrorMiddleware : IMiddleware
{
    public readonly IUnitOfWork _unitOfWork;

    public ErrorMiddleware(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ErrorLogs errorLogs = new ErrorLogs()
            {
                Message = ex.Message,
                RequestMethod = context.Request.Method,
                RequestPath = context.Request.Path,
                Trace = ex.StackTrace.Split(" at")[1].Split("\\")[ex.StackTrace.Split(" at")[1].Split("\\").Length - 1].Replace("\r\n", "").Trim()
            };

            var result = await _unitOfWork.Repository.AddAsync<ErrorLogs, int>(errorLogs);

            await _unitOfWork.Repository.CompleteAsync();

            

        }
    }
}
