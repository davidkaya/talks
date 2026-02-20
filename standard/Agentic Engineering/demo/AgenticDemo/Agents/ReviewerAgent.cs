using Microsoft.SemanticKernel.ChatCompletion;

namespace AgenticDemo.Agents;

/// <summary>
/// Critiques articles and provides actionable feedback for improvement.
/// </summary>
public class ReviewerAgent(IChatCompletionService chatService)
{
    private const string SystemPrompt = """
        You are a Reviewer Agent. Your job is to critically evaluate articles and
        provide specific, actionable feedback. Focus on:
        1. Accuracy — are the facts correct?
        2. Clarity — is the writing clear and easy to follow?
        3. Structure — is the article well-organized?
        4. Completeness — are there important points missing?
        5. Engagement — is it interesting to read?

        Provide 3-5 specific improvement suggestions. Be constructive, not harsh.
        """;

    public async Task<string> ReviewArticleAsync(string article)
    {
        var history = new ChatHistory(SystemPrompt);
        history.AddUserMessage(
            $"""
            Please review the following article and provide specific feedback:

            {article}
            """);

        var response = await chatService.GetChatMessageContentAsync(history);
        return response.Content ?? "No review generated.";
    }
}
