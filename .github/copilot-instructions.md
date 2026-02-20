# Copilot Instructions for the Talks Repository

This repository contains technical talks, presentations, and accompanying demo projects. Follow these instructions when helping prepare new talks, presentations, or demos.

---

## Repository Structure

Talks are organized by **category**, then by **talk name**:

```
<category>/
  <Talk Name>/
    presentation.md      ← The slide content (Markdown)
    demo/                ← Demo project(s) for live coding
      <ProjectName>/     ← One or more runnable projects
```

### Categories

| Folder       | Description                              | Typical duration |
|--------------|------------------------------------------|------------------|
| `lightning/` | Lightning talks — short, focused, punchy | 5–15 minutes     |
| `standard/`  | Standard talks — deeper, more thorough   | 30–45 minutes    |

When new categories are needed (e.g., `conference/`, `workshop/`, `meetup/`), follow the same nested structure.

---

## Tooling

| Tool | Purpose |
|------|---------|
| [Bun](https://bun.sh) | JavaScript package manager and runtime — use `bun install`, `bun run <script>` |
| [Slidev](https://sli.dev) | Markdown-to-slides presentation framework |
| [.NET SDK](https://dotnet.microsoft.com) | Build and run demo projects |

> ⚠️ Always use **bun** (not npm/yarn/pnpm) for managing JavaScript dependencies in this repository.

---

## Presentations (`presentation.md`)

All presentations are written in **Markdown** for [Slidev](https://sli.dev), a developer-friendly, Markdown-based presentation framework.

### Slidev Frontmatter

Every `presentation.md` **must** start with a YAML frontmatter block configuring Slidev:

```yaml
---
theme: default
title: Talk Title
info: |
  A brief description of the talk.
highlighter: shiki
transition: slide-left
---
```

Required frontmatter fields:

| Field | Value | Purpose |
|-------|-------|---------|
| `theme` | `default` (or another installed theme) | Slidev theme |
| `title` | The talk title | Browser tab title and metadata |
| `info` | Multi-line description | Talk description shown in presenter view |
| `highlighter` | `shiki` | Syntax highlighter for code blocks |
| `transition` | `slide-left` | Default slide transition |

### Slide Separators

- Use `---` (horizontal rules) to separate slides
- The first `---` block is the YAML frontmatter; subsequent `---` lines are slide separators
- You can add per-slide frontmatter between `---` markers:

```markdown
---
layout: center
---

# Centered Slide Title
```

### Available Layouts

Use Slidev layouts for visual variety (set via per-slide frontmatter):

| Layout | Use for |
|--------|---------|
| `default` | Standard content slides |
| `cover` | Title/opening slides |
| `center` | Centered content (quotes, key insights) |
| `two-cols` | Side-by-side comparisons |
| `image-right` | Content with image on right |
| `section` | Section divider slides |
| `end` | Closing slide |

### Structure

Every presentation should follow this outline:

1. **Title slide** — `# Talk Title` as the H1
2. **Agenda** — Numbered list of all sections
3. **Numbered sections** — Each section is an `## N. Section Title`
4. **Key Takeaways** — A summary table near the end
5. **Resources** — Links to official docs, further reading, and the demo project

### Writing Style

- **Tone**: Educational, practical, and conversational — like explaining to a smart colleague
- **Use mental models and analogies** to make abstract concepts concrete (e.g., "Think of middleware like layers of an onion")
- **Include code in every section** — this is a technical audience; show, don't just tell
- **Use emoji** for visual scanning:
  - ✅ for correct examples or positive points
  - ❌ for anti-patterns, mistakes, or pitfalls
  - ⚠️ for warnings and common mistakes
  - 💡 for key insights and tips
  - ⭐ for highlighted/recommended approaches
- **Use tables** for comparisons, summaries, and quick-reference information
- **Use ASCII diagrams** for architecture and flow visualization (no external image dependencies)
- **Include "Common Pitfalls" and "Best Practices" sections** — audiences love practical do's and don'ts
- **End with a numbered Key Takeaways table** (columns: `#`, `Takeaway`)
- **Separate slides/sections with `---`** (horizontal rules)

### Formatting Rules

- Use fenced code blocks with **language identifiers** (e.g., ` ```csharp `)
- Use `>` blockquotes for callouts, rules of thumb, and key insights
- Keep bullet points concise — no paragraphs inside bullets
- Use **bold** for emphasis on key terms on first introduction
- Use `inline code` for type names, method names, and technical identifiers

---

## Demo Projects

Demo projects live under `<Talk Name>/demo/` and are meant for **live coding during the presentation**.

### General Principles

- **Demos must be self-contained** — clone and run with no external dependencies beyond the SDK
- **Demos must be simple** — strip away anything not directly relevant to the talk topic
- **Demos must work** — always keep demo projects in a runnable state
- **No authentication to external services** — use in-memory data, hardcoded values, or local resources
- **Include an `.http` file** (or equivalent) for API demos so endpoints can be tested quickly

### Code Style (C# / .NET)

The primary tech stack is **.NET** with **C#**. Follow these conventions:

- **Target the latest .NET version** (currently .NET 10)
- **Enable nullable reference types** (`<Nullable>enable</Nullable>`)
- **Enable implicit usings** (`<ImplicitUsings>enable</ImplicitUsings>`)
- **Use file-scoped namespaces** (`namespace X;` not `namespace X { }`)
- **Use top-level statements** in `Program.cs` (no explicit `Main` method)
- **Use records** for simple data types (e.g., `record WeatherForecast(...)`)
- **Use primary constructors** where appropriate
- **Use expression-bodied members** for single-line methods and properties
- **Add XML doc comments** (`/// <summary>`) on public classes — include **what the class does** and **what concept it demonstrates** (e.g., "Demonstrates: short-circuiting the pipeline")
- **Add inline comments** only to explain *why*, not *what* — focus on teaching moments relevant to the talk
- **Organize related code in feature folders** (e.g., `Middleware/` folder for all middleware classes)
- **Use extension methods** for clean registration (e.g., `UseMyMiddleware()`)

### Code Comments for Demos

Since demos are educational, comments should be more verbose than production code:

- Use comments to **label the "before" and "after"** sections (e.g., `// Work BEFORE the next middleware`)
- Use `// ⚠️` prefix for important caveats
- Use `// ❌` and `// ✅` to show wrong vs. right approaches inline

---

## Creating a New Talk

When asked to create a new talk, follow these steps:

1. **Determine the category** — ask if unsure (lightning, standard, conference, workshop, etc.)
2. **Create the folder structure**:
   ```
   <category>/<Talk Name>/
     README.md
     presentation.md
     demo/
       <ProjectName>/
   ```
3. **Write a `README.md`** for the talk with a brief title, description, topic list, and instructions to run the demo
4. **Write `presentation.md`** with Slidev YAML frontmatter at the top, following the structure and style above
5. **Create the demo project** using `dotnet new` (or equivalent for the target stack)
6. **Ensure the demo compiles and runs** before considering the talk complete
7. **Update the root `README.md`** — add the new talk to the appropriate category table
8. **Update `package.json`** — add `slides:<name>`, `build:<name>`, and `export:<name>` scripts for the new talk, then run `bun install`
9. **Review all generated content** — re-read the presentation, README, and demo code to verify:
   - All technical claims, API names, and code samples are **accurate and up-to-date**
   - Code examples **compile and behave as described**
   - There are no **factual errors, outdated syntax, or misleading statements**
   - Slide ordering and agenda match the actual content
   - Links and references point to valid resources

---

## README.md

The root `README.md` should list all talks with links. When adding a new talk, add it under the appropriate category heading.

---

## Git Commits

All commits **must** follow the [Conventional Commits](https://www.conventionalcommits.org/) specification.

### Format

```
<type>(<optional scope>): <description>

[optional body]

[optional footer(s)]
```

### Allowed Types

| Type       | When to use                                      |
|------------|--------------------------------------------------|
| `feat`     | A new talk, presentation, or demo                |
| `fix`      | Bug fix in a demo or correction in a presentation|
| `docs`     | Documentation-only changes (README, instructions)|
| `style`    | Formatting, whitespace, missing semicolons, etc. |
| `refactor` | Code restructuring without behavior change       |
| `chore`    | Maintenance tasks, CI, tooling, dependencies     |

### Rules

- **Commits must be atomic** — each commit should contain exactly one logical change (e.g., don't mix a new demo with a README update in the same commit)
- **Type is required** — never commit without a type prefix
- **Use lowercase** for type and description
- **Use imperative mood** in the description (e.g., "add", not "added" or "adds")
- **Scope is optional** but encouraged — use the talk name or category (e.g., `feat(middlewares): add ordering demo`)
- **Breaking changes** must include `BREAKING CHANGE:` in the footer or `!` after the type/scope

### Examples

```
feat(middlewares): add custom middleware demo project
fix(middlewares): correct middleware ordering in pipeline
docs: update README with new talk listing
chore: add gitignore rules for build output
```

---

## Non-.NET Talks

If a talk covers a different technology (TypeScript, Python, Go, etc.), adapt the demo conventions accordingly:

- Use the idiomatic project structure for that language
- Keep the same principles: self-contained, simple, runnable, well-commented for teaching
- Use the standard package manager and build tool for that ecosystem
