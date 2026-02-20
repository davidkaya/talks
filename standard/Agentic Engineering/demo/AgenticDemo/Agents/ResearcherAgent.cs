using Microsoft.SemanticKernel.ChatCompletion;

namespace AgenticDemo.Agents;

/// <summary>
/// Gathers key facts, talking points, and structure for a given topic.
/// </summary>
public class ResearcherAgent(IChatCompletionService chatService)
{
    private const string SystemPrompt = """
        You are a Research Agent. Your job is to gather key facts, talking points,
        and a logical structure for a given topic. Provide a well-organized outline
        with 5-7 key points, each with a brief explanation. Be factual and concise.
        Do not write a full article — just the research notes and outline.
        """;

    public async Task<string> ResearchTopicAsync(string topic)
    {
        var history = new ChatHistory(SystemPrompt);
        history.AddUserMessage($"Research the following topic and provide key points and structure: {topic}");

        var response = await chatService.GetChatMessageContentAsync(history);
        return response.Content ?? "No research results generated.";
    }
}
