using ApiContracts;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace Booking.MiddleWares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ApplicationException ex)
        {
            _logger.LogWarning(ex, "Application-specific error occurred: {Message}", ex.Message);
            await HandleErrorAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access error: {Message}", ex.Message);
            await HandleErrorAsync(context, HttpStatusCode.Unauthorized, "You are not authorized to perform this action.");
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Operation not allowed: {Message}", ex.Message);
            await HandleErrorAsync(context, HttpStatusCode.Forbidden, "You do not have permission to perform this action.");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Resource not found: {Message}", ex.Message);
            await HandleErrorAsync(context, HttpStatusCode.NotFound, "The requested resource was not found.");
        }
        catch (PayloadTooLargeException ex)
        {
            _logger.LogWarning(ex, "Payload too large: {Message}", ex.Message);
            await HandleErrorAsync(context, HttpStatusCode.RequestEntityTooLarge, "The request payload is too large.");
        }
        catch (UnsupportedMediaTypeException ex)
        {
            _logger.LogWarning(ex, "Unsupported media type: {Message}", ex.Message);
            await HandleErrorAsync(context, HttpStatusCode.UnsupportedMediaType, "The request format is not supported.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict: {Message}", ex.Message);
            await HandleErrorAsync(context, HttpStatusCode.Conflict, "A concurrency conflict occurred. Please try again.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);

            var (statusCode, errorMessage) = DetermineSpecificErrors(ex);

            await HandleErrorAsync(context, statusCode, errorMessage);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, HttpStatusCode statusCode, string errorMessage)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var errorResponse = new GResponse<bool>
        {
            Error = null,
            StatusCode = context.Response.StatusCode.ToString(),
            ErrorMessage = errorMessage,
            Data = false,
            IsSucceeded = false
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    }

    private (HttpStatusCode, string) DetermineSpecificErrors(Exception ex)
    {
        if (ex.InnerException != null)
        {
            string innerMessage = ex.InnerException.Message;

            if (innerMessage.Contains("Cannot insert duplicate key"))
            {
                return (HttpStatusCode.Conflict, "Duplicate entry detected. The resource already exists.");
            }

            if (innerMessage.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
            {
                return (HttpStatusCode.Conflict, "Deletion failed. The resource has related dependent entries.");
            }

            if (innerMessage.Contains("timeout"))
            {
                return (HttpStatusCode.RequestTimeout, "The operation timed out. Please try again later.");
            }
        }

        return (HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again later.");
    }
}