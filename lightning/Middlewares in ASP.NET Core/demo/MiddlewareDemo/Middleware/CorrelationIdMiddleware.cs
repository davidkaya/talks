namespace MiddlewareDemo.Middleware;

/// <summary>
/// Attaches a unique correlation ID to every request/response for distributed tracing.
/// Demonstrates: reading/writing headers, passing data through the pipeline.
/// </summary>
public class CorrelationIdMiddleware
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;

    public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Use existing correlation ID from the request header, or generate a new one
        if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId)
            || string.IsNullOrWhiteSpace(correlationId))
        {
            correlationId = Guid.NewGuid().ToString("N")[..8]; // short ID for demo
        }

        // Store it in HttpContext.Items so other middleware/endpoints can access it
        context.Items["CorrelationId"] = correlationId.ToString();

        // Add it to the response headers
        context.Response.OnStarting(() =>
        {
            context.Response.Headers[CorrelationIdHeader] = correlationId.ToString();
            return Task.CompletedTask;
        });

        var id = correlationId.ToString();
        _logger.LogInformation("[{CorrelationId}] Request started", id);

        await _next(context);

        _logger.LogInformation("[{CorrelationId}] Request finished", id);
    }
}

public static class CorrelationIdMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
        => builder.UseMiddleware<CorrelationIdMiddleware>();
}
