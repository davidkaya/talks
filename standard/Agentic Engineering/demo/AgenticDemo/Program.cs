using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using AgenticDemo.Agents;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables()
    .Build();

var apiKey = configuration["OpenAI:ApiKey"]
    ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY")
    ?? throw new InvalidOperationException(
        "OpenAI API key not found. Set it via user secrets (OpenAI:ApiKey) or environment variable (OPENAI_API_KEY).");

var modelId = configuration["OpenAI:ModelId"] ?? "gpt-4o-mini";

var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(modelId, apiKey)
    .Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();

var researcher = new ResearcherAgent(chatService);
var writer = new WriterAgent(chatService);
var reviewer = new ReviewerAgent(chatService);

Console.WriteLine("╔══════════════════════════════════════════════╗");
Console.WriteLine("║   🤖 Agentic Engineering Demo                ║");
Console.WriteLine("║   Multi-Agent Collaboration with             ║");
Console.WriteLine("║   Semantic Kernel                            ║");
Console.WriteLine("╚══════════════════════════════════════════════╝");
Console.WriteLine();

Console.Write("Enter a topic for the agents to write about: ");
var topic = Console.ReadLine()?.Trim();

if (string.IsNullOrWhiteSpace(topic))
{
    topic = "Kubernetes";
    Console.WriteLine($"No topic provided. Using default: \"{topic}\"");
}

Console.WriteLine();
Console.WriteLine("═══════════════════════════════════════════════");

// Step 1: Researcher gathers key points
Console.WriteLine();
Console.WriteLine("📚 STEP 1: Researcher Agent — Gathering key points...");
Console.WriteLine("───────────────────────────────────────────────");
var research = await researcher.ResearchTopicAsync(topic);
Console.WriteLine(research);

// Step 2: Writer creates a draft based on research
Console.WriteLine();
Console.WriteLine("✍️  STEP 2: Writer Agent — Drafting article...");
Console.WriteLine("───────────────────────────────────────────────");
var draft = await writer.WriteArticleAsync(topic, research);
Console.WriteLine(draft);

// Step 3: Reviewer critiques the draft
Console.WriteLine();
Console.WriteLine("🔍 STEP 3: Reviewer Agent — Critiquing draft...");
Console.WriteLine("───────────────────────────────────────────────");
var feedback = await reviewer.ReviewArticleAsync(draft);
Console.WriteLine(feedback);

// Step 4: Writer revises based on feedback (Reflection pattern)
Console.WriteLine();
Console.WriteLine("🔄 STEP 4: Writer Agent — Revising based on feedback (Reflection)...");
Console.WriteLine("───────────────────────────────────────────────");
var revisedArticle = await writer.ReviseArticleAsync(draft, feedback);
Console.WriteLine(revisedArticle);

Console.WriteLine();
Console.WriteLine("═══════════════════════════════════════════════");
Console.WriteLine("✅ Multi-agent workflow complete!");
Console.WriteLine();
Console.WriteLine("Patterns demonstrated:");
Console.WriteLine("  • Multi-Agent Collaboration — 3 specialized agents");
Console.WriteLine("  • Planning — Researcher decomposed the topic");
Console.WriteLine("  • Reflection — Reviewer critiqued, Writer improved");
Console.WriteLine("  • Tool Use — Semantic Kernel plugin architecture");
