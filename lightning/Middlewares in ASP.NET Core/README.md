# Middlewares in ASP.NET Core

A lightning talk exploring how the ASP.NET Core request pipeline works and how to write custom middleware.

## Topics Covered

- What middleware is and how the request pipeline works
- The three delegates: `Use`, `Run`, `Map`
- Built-in middlewares
- Writing custom middleware (inline & class-based)
- Middleware ordering and why it matters
- Short-circuiting and branching the pipeline
- Middleware vs Filters — when to use what
- Common pitfalls & best practices

## Running the Demo

```bash
cd demo/MiddlewareDemo
dotnet run
```

Use the included `MiddlewareDemo.http` file to test the endpoints.
