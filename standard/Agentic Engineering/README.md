# Agentic Engineering

A standard-length meetup talk (30-45 min) exploring how software engineering is evolving from
writing code to orchestrating AI agents — what it is, how it works, and what developers need to
know.

## Topics Covered

- What Agentic Engineering is and how it differs from traditional coding and vibe coding
- The evolution timeline: Coding → Vibe Coding → Agentic Engineering
- Andrew Ng's four agentic design patterns: Reflection, Tool Use, Planning, Multi-Agent
  Collaboration
- Agent architecture: the Perceive → Plan → Act → Reflect loop
- Agentic coding tools landscape (Copilot Agent, Cursor, Windsurf, Claude Code, Devin, Codex)
- Agentic frameworks: LangGraph, AutoGen, CrewAI, Semantic Kernel
- Open protocols: MCP (Anthropic) and A2A (Google)
- Best practices, risks, challenges, and governance
- The developer's evolving role — from code writer to orchestrator

## Running the Demo

The demo is a .NET 8 console application using Microsoft Semantic Kernel that demonstrates a
multi-agent workflow with three specialized agents (Researcher, Writer, Reviewer).

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An OpenAI API key

### Setup

```bash
cd demo/AgenticDemo
dotnet user-secrets set "OpenAI:ApiKey" "your-api-key-here"
dotnet run
```

Alternatively, set the `OPENAI_API_KEY` environment variable:

```bash
export OPENAI_API_KEY="your-api-key-here"
cd demo/AgenticDemo
dotnet run
```
