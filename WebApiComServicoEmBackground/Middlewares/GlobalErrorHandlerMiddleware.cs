using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebApiComServicoEmBackground;

public class GlobalErrorHandlerMiddleware
{
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public GlobalErrorHandlerMiddleware(
        RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;
            
            var problem = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = "An error has occurred."
            };

            string result = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(result);
        }
    }
}