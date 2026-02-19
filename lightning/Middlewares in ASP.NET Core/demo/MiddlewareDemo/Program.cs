using MiddlewareDemo.Middleware;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// ──────────────────────────────────────────────────────────────
// MIDDLEWARE PIPELINE – order matters!
// ──────────────────────────────────────────────────────────────

// 1. Global exception handling (outermost – catches everything)
app.UseGlobalExceptionHandling();

// 2. Correlation ID (early – so all logs/responses include it)
app.UseCorrelationId();

// 3. Request timing (measures everything downstream)
app.UseRequestTiming();

// 4. Inline middleware example – simple request logging
app.Use(async (context, next) =>
{
    Console.WriteLine($"[Inline] Processing: {context.Request.Method} {context.Request.Path}");
    await next(context);
    Console.WriteLine($"[Inline] Done: {context.Response.StatusCode}");
});

app.UseHttpsRedirection();

// 5. Conditional middleware – API key only for /secure routes (UseWhen rejoins the pipeline)
app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/secure"),
    branch => branch.UseApiKeyValidation());

// ──────────────────────────────────────────────────────────────
// ENDPOINTS
// ──────────────────────────────────────────────────────────────

// Basic endpoint – no auth required
app.MapGet("/", () => "Hello! Visit /weatherforecast, /secure/data, or /throw to test middleware.");

// Standard endpoint
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();
    return forecast;
});

// Protected endpoint – requires X-Api-Key header
app.MapGet("/secure/data", (HttpContext context) =>
{
    var correlationId = context.Items["CorrelationId"]?.ToString();
    return Results.Ok(new
    {
        message = "You have access to secure data!",
        correlationId,
        timestamp = DateTime.UtcNow
    });
});

// Endpoint that throws – demonstrates GlobalExceptionMiddleware
app.MapGet("/throw", () =>
{
    throw new InvalidOperationException("This is a test exception to demonstrate error-handling middleware.");
});

// Pipeline branching with Map – completely separate branch
app.Map("/health", healthApp =>
{
    healthApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { status = "healthy", timestamp = DateTime.UtcNow });
    });
});

// Terminal middleware with Run (never calls next)
app.Map("/terminal", terminalApp =>
{
    terminalApp.Run(async context =>
    {
        await context.Response.WriteAsync("This is a terminal middleware – pipeline stops here.");
    });
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
