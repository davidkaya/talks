# 🎤 Talks

A collection of technical talks, presentations, and live-coding demos.

Each talk includes a **Markdown-based slide deck** (compatible with [Marp](https://marp.app) / [Slidev](https://sli.dev)) and a **self-contained demo project** you can clone and run.

---

## ⚡ Lightning Talks

Short, focused talks — 5 to 15 minutes.

| Talk | Description | Stack |
|------|-------------|-------|
| [Middlewares in ASP.NET Core](lightning/Middlewares%20in%20ASP.NET%20Core/presentation.md) | How the request pipeline works, `Use`/`Run`/`Map` delegates, writing custom middleware, and ordering pitfalls | .NET / C# |
| [Iterating in C#](lightning/Iterating%20in%20C%23/presentation.md) | All the ways to loop in C# — `for`, `foreach`, LINQ, `Span<T>`, `await foreach`, parallel, and custom iterators | .NET / C# |

---

## 📁 Repository Structure

```
<category>/
  <Talk Name>/
    presentation.md      ← Slide content (Markdown)
    demo/
      <ProjectName>/     ← Runnable demo project
```

---

## 🚀 Running a Demo

1. Navigate to the demo folder, e.g. `lightning/Middlewares in ASP.NET Core/demo/MiddlewareDemo`
2. Run `dotnet run`
3. Open the `.http` file (if included) to test endpoints

---

## 📝 License

This repository is for personal and educational use.