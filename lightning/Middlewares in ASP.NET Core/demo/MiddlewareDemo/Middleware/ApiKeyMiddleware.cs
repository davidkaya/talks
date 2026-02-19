namespace MiddlewareDemo.Middleware;

/// <summary>
/// Validates an API key header on protected routes.
/// Demonstrates: short-circuiting the pipeline (not calling next).
/// </summary>
public class ApiKeyMiddleware
{
    private const string ApiKeyHeader = "X-Api-Key";
    private const string ExpectedApiKey = "demo-secret-key-12345";

    private readonly RequestDelegate _next;
    private readonly ILogger<ApiKeyMiddleware> _logger;

    public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var apiKey)
            || apiKey != ExpectedApiKey)
        {
            _logger.LogWarning("Unauthorized request – missing or invalid API key");

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Unauthorized",
                message = $"A valid '{ApiKeyHeader}' header is required."
            });

            // ⚠️ NOT calling _next(context) – this short-circuits the pipeline
            return;
        }

        _logger.LogInformation("API key validated successfully");
        await _next(context);
    }
}

public static class ApiKeyMiddlewareExtensions
{
    public static IApplicationBuilder UseApiKeyValidation(this IApplicationBuilder builder)
        => builder.UseMiddleware<ApiKeyMiddleware>();
}
