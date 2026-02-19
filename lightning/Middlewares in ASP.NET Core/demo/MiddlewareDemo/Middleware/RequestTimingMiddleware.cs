using System.Diagnostics;

namespace MiddlewareDemo.Middleware;

/// <summary>
/// Measures and logs how long each request takes to process.
/// Demonstrates: class-based middleware with constructor injection.
/// </summary>
public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // Work BEFORE the next middleware
        _logger.LogInformation(">> {Method} {Path} started", context.Request.Method, context.Request.Path);

        await _next(context);

        // Work AFTER the next middleware
        stopwatch.Stop();
        _logger.LogInformation("<< {Method} {Path} completed in {Elapsed}ms with status {Status}",
            context.Request.Method, context.Request.Path,
            stopwatch.ElapsedMilliseconds, context.Response.StatusCode);
    }
}

public static class RequestTimingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder builder)
        => builder.UseMiddleware<RequestTimingMiddleware>();
}
