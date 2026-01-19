using System.Net;
using System.Text.Json;
using TaskTracker.Application.Exceptions;

namespace TaskTracker.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception occurred");

        var response = context.Response;
        response.ContentType = "application/json";

        var (statusCode, title, details) = exception switch
        {
            NotFoundException => 
                ((int)HttpStatusCode.NotFound, "Not Found", exception.Message),
            
            ValidationException validationEx =>
                ((int)HttpStatusCode.BadRequest, "Validation Error", 
                 string.Join("; ", validationEx.Errors.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}"))),
            
            _ => 
                ((int)HttpStatusCode.InternalServerError, "Internal Server Error",
                 _env.IsDevelopment() ? exception.Message : "An unexpected error occurred")
        };

        response.StatusCode = statusCode;

        var errorResponse = new
        {
            Status = statusCode,
            Title = title,
            Detail = details,
            TraceId = context.TraceIdentifier
        };

        var json = JsonSerializer.Serialize(errorResponse);
        await response.WriteAsync(json);
    }
}

public class ValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors) 
        : base("One or more validation errors occurred")
    {
        Errors = errors;
    }
}