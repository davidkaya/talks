# 🎤 Talks

A collection of technical talks, presentations, and live-coding demos.

Each talk includes a **Markdown-based slide deck** powered by [Slidev](https://sli.dev) and a **self-contained demo project** you can clone and run.

---

## ⚡ Lightning Talks

Short, focused talks — 5 to 15 minutes.

| Talk | Description | Stack |
|------|-------------|-------|
| [Middlewares in ASP.NET Core](lightning/Middlewares%20in%20ASP.NET%20Core/presentation.md) | How the request pipeline works, `Use`/`Run`/`Map` delegates, writing custom middleware, and ordering pitfalls | .NET / C# |
| [Iterating in C#](lightning/Iterating%20in%20C%23/presentation.md) | All the ways to loop in C# — `for`, `foreach`, LINQ, `Span<T>`, `await foreach`, parallel, and custom iterators | .NET / C# |

---

## 📢 Standard Talks

Deeper talks — 30 to 45 minutes.

| Talk | Description | Stack |
|------|-------------|-------|
| [Agentic Engineering](standard/Agentic%20Engineering/presentation.md) | How software engineering is evolving from writing code to orchestrating AI agents — design patterns, frameworks, protocols (MCP & A2A), tools, and best practices | AI / .NET / Semantic Kernel |

---

## 📁 Repository Structure

```
<category>/
  <Talk Name>/
    presentation.md      ← Slidev slide deck (Markdown + YAML frontmatter)
    demo/
      <ProjectName>/     ← Runnable demo project
```

---

## 🖥️ Running a Presentation

Presentations use [Slidev](https://sli.dev). Install dependencies once with [Bun](https://bun.sh), then run any talk:

```bash
bun install
```

| Command | Talk |
|---------|------|
| `bun run slides:middlewares` | Middlewares in ASP.NET Core |
| `bun run slides:iterating` | Iterating in C# |
| `bun run slides:agentic` | Agentic Engineering |

You can also build a static SPA or export to PDF:

```bash
bun run build:middlewares    # Static SPA → dist/
bun run export:middlewares   # PDF export
```

---

## 🚀 Running a Demo

1. Navigate to the demo folder, e.g. `lightning/Middlewares in ASP.NET Core/demo/MiddlewareDemo`
2. Run `dotnet run`
3. Open the `.http` file (if included) to test endpoints

---

## 📝 License

This repository is for personal and educational use.