
using ApiContracts;
using System.Net;

namespace Booking.MiddleWares;

public class GlobalEaxceptionErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalEaxceptionErrorHandlingMiddleware> _logger;

    public GlobalEaxceptionErrorHandlingMiddleware(ILogger<GlobalEaxceptionErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next (context);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 404;
            var problemDetails = new GResponse<bool>
            {
                Error = null,
                StatusCode = context.Response.StatusCode.ToString(),
                ErrorMessage = ex.Message,
                Data = false,
                IsSucceeded = false
            };
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            if (ex.InnerException != null)
            {
                _logger.LogError(ex, ex.Message);

                if (ex.InnerException.ToString().Contains("Cannot insert duplicate key"))
                {
                    _logger.LogError(ex, ex.Message);

                    context.Response.StatusCode = 409;
                    var problemDetail=new GResponse<bool>
                    {
                        Error = null,
                        StatusCode = context.Response.StatusCode.ToString(),
                        ErrorMessage = "Cannot insert duplicate key",
                        Data = false,
                        IsSucceeded = false
                    };
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(problemDetail);
                }
               else if(ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
               {
                    _logger.LogError(ex, ex.Message);

                    context.Response.StatusCode = 408;
                    var problemDetail = new GResponse<bool>
                    {
                        Error = null,
                        StatusCode = context.Response.StatusCode.ToString(),
                        ErrorMessage = "Cannot Remove That Because That Is Has A Childrens",
                        Data = false,
                        IsSucceeded = false
                    };
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(problemDetail);
               }
                else
                {
                    _logger.LogError(ex, ex.Message);
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var problemDetailss = new GResponse<bool>
                    {
                        Error = null,
                        StatusCode = context.Response.StatusCode.ToString(),
                        ErrorMessage = "Internal Server Error",
                        Data = false,
                        IsSucceeded = false
                    };
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(problemDetailss);
                }
                
            }
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var problemDetails = new GResponse<bool>
            {
                Error = null,
                StatusCode = context.Response.StatusCode.ToString(),
                ErrorMessage = "Internal Server Error",
                Data = false,
                IsSucceeded = false
            };
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(problemDetails);

        }
    }
}
