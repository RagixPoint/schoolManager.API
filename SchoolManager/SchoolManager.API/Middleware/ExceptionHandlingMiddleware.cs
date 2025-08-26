using System.Net;
using System.Text.Json;
using SchoolManager.Application.Common.Exceptions;

namespace SchoolManager.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Une erreur est survenue: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            InvalidCredentialsException => new
            {
                status = (int)HttpStatusCode.BadRequest,
                error = "credentials.error",
                message = exception.Message
            },
            AccountInactiveException => new
            {
                status = (int)HttpStatusCode.Forbidden,
                error = "credentials.error.inactive",
                message = exception.Message
            },
            EmailAlreadyExistsException => new {
                status = (int)HttpStatusCode.Conflict,
                error = "email.already.exists",
                message = exception.Message
            },
            _ => new
            {
                status = (int)HttpStatusCode.InternalServerError,
                error = "error.internal",
                message = "error.internal.message"
            }
        };

        context.Response.StatusCode = response.status;

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}